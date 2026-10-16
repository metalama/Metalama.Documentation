// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace BuildMetalamaDocumentation.SourceBrowser;

/// <summary>
/// A directory whose Markdown files can declare browsable source directories, and the name of this directory
/// in the URL of the source browser.
/// </summary>
/// <param name="Path">The path of the directory, relative to the repository.</param>
/// <param name="Name">The name of the directory in the site, for instance <c>samples</c> for <c>source/samples/...</c>.</param>
internal sealed record SourceBrowserRoot( string Path, string Name );

/// <summary>
/// Generates the files of the HelpBrowser source browser before DocFx runs. An article declares the source directories
/// shown under it in the navigation with the <c>browse-source</c> key of its front matter, whose value is a path, a list
/// of paths, or a list of nodes with a <c>path</c>, an optional <c>title</c> and optional <c>children</c>, all relative
/// to the article. For each declared directory, the generator copies the raw files and the syntax-highlighted HTML files
/// produced by the Metalama HTML writer under <see cref="OutputDirectory"/>, and writes <c>manifest.json</c>, which lists
/// the directories and the navigation nodes that show them. DocFx publishes <see cref="OutputDirectory"/> as the <c>source</c> directory of the site.
/// The generator also writes <see cref="IndexFile"/>, which maps local directories to their site path, so that the
/// Markdig extensions can link code samples to the source browser.
/// </summary>
internal sealed partial class SourceBrowserGenerator
{
    public const string SitePathPrefix = "source";

    private static readonly HashSet<string> _excludedDirectoryNames = new( StringComparer.OrdinalIgnoreCase )
    {
        "bin", "obj", "node_modules", "TestResults"
    };

    private static readonly HashSet<string> _excludedExtensions = new( StringComparer.OrdinalIgnoreCase ) { ".user", ".suo", ".binlog" };

    // Markdown files are the articles that explain the code. They are published as articles, not as source files.
    private static readonly HashSet<string> _excludedSourceExtensions = new( StringComparer.OrdinalIgnoreCase ) { ".md", ".mdx" };

    private readonly string _repoDirectory;
    private readonly IReadOnlyList<SourceBrowserRoot> _roots;
    private readonly IReadOnlyList<string> _referenceDirectories;
    private readonly Action<string> _writeWarning;
    private readonly Dictionary<string, string?> _gitRepoUrls = new( StringComparer.OrdinalIgnoreCase );

    /// <param name="repoDirectory">The root directory of the documentation repository.</param>
    /// <param name="roots">The directories whose articles can declare source directories.</param>
    /// <param name="referenceDirectories">The directories, relative to the repository, whose articles are scanned for
    /// code inclusions, in addition to <paramref name="roots"/>, to find the articles that include each file.</param>
    /// <param name="writeWarning">Reports a warning.</param>
    public SourceBrowserGenerator(
        string repoDirectory,
        IReadOnlyList<SourceBrowserRoot> roots,
        IReadOnlyList<string> referenceDirectories,
        Action<string> writeWarning )
    {
        this._repoDirectory = repoDirectory;
        this._roots = roots;
        this._referenceDirectories = referenceDirectories;
        this._writeWarning = writeWarning;
    }

    /// <summary>
    /// Gets the directory published as the <c>source</c> directory of the site. It must match <c>docfx.json</c>.
    /// </summary>
    public static string GetOutputDirectory( string repoDirectory ) => Path.Combine( repoDirectory, "artifacts", "source-browser" );

    /// <summary>
    /// Gets the file that maps local directories to site paths. It is not published.
    /// </summary>
    public static string GetIndexFile( string repoDirectory ) => Path.Combine( repoDirectory, "artifacts", "source-browser.index.json" );

    private string OutputDirectory => GetOutputDirectory( this._repoDirectory );

    private string IndexFile => GetIndexFile( this._repoDirectory );

    /// <summary>
    /// Generates the files and returns the manifest.
    /// </summary>
    public SourceBrowserManifest Generate()
    {
        if ( Directory.Exists( this.OutputDirectory ) )
        {
            Directory.Delete( this.OutputDirectory, true );
        }

        Directory.CreateDirectory( this.OutputDirectory );

        var (declaredDirectories, mounts) = this.FindMounts();
        var references = this.FindReferences();

        var directories = declaredDirectories
            .OrderBy( d => d.SitePath, StringComparer.Ordinal )
            .Select( d => this.GenerateDirectory( d, declaredDirectories, references ) )
            .ToList();

        var manifest = new SourceBrowserManifest( 2, directories, mounts );

        File.WriteAllText( Path.Combine( this.OutputDirectory, "manifest.json" ), JsonSerializer.Serialize( manifest, _jsonOptions ) );

        var index = new SourceBrowserIndexData(
            declaredDirectories.Select( d => new SourceBrowserIndexEntry( d.FullPath, d.SitePath ) ).ToList() );

        File.WriteAllText( this.IndexFile, JsonSerializer.Serialize( index, _jsonOptions ) );

        return manifest;
    }

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    private sealed record DeclaredDirectory( string FullPath, string SitePath );

    /// <summary>
    /// A node declared by the <c>browse-source</c> key, before its path is resolved.
    /// </summary>
    private sealed record MountDeclaration( string? Title, string? Path, IReadOnlyList<MountDeclaration> Children );

    /// <summary>
    /// Finds the source directories and the navigation nodes declared by the <c>browse-source</c> front matter key of the
    /// articles. The key accepts a path, a list of paths, or a list of nodes with a <c>path</c>, an optional
    /// <c>title</c> and optional <c>children</c>, all relative to the article.
    /// </summary>
    private (List<DeclaredDirectory> Directories, List<SourceBrowserMount> Mounts) FindMounts()
    {
        var directories = new Dictionary<string, DeclaredDirectory>( StringComparer.OrdinalIgnoreCase );
        var mounts = new List<SourceBrowserMount>();

        foreach ( var root in this._roots )
        {
            var rootDirectory = Path.GetFullPath( Path.Combine( this._repoDirectory, root.Path ) );

            if ( !Directory.Exists( rootDirectory ) )
            {
                this._writeWarning( $"The source browser root '{rootDirectory}' does not exist." );

                continue;
            }

            foreach ( var markdownFile in EnumerateFiles( rootDirectory, "*.md" ) )
            {
                var frontMatter = FrontMatterReader.ReadMapping( markdownFile );

                if ( frontMatter == null || !frontMatter.Children.TryGetValue( new YamlScalarNode( "browse-source" ), out var sourceNode ) )
                {
                    continue;
                }

                if ( !frontMatter.Children.TryGetValue( new YamlScalarNode( "uid" ), out var uidNode ) || uidNode is not YamlScalarNode { Value: { } uid } )
                {
                    this._writeWarning( $"'{markdownFile}' declares a source directory but has no uid." );

                    continue;
                }

                var declarations = ParseMountDeclarations( sourceNode );

                // A single top-level node is the source code of the article. When there are several, each one is
                // named after its directory, unless the article gives a title.
                var defaultTitle = declarations.Count == 1 ? "Source code" : null;

                foreach ( var declaration in declarations )
                {
                    if ( ResolveMount( declaration, uid, defaultTitle ) is { } mount )
                    {
                        mounts.Add( mount );
                    }
                }

                SourceBrowserMount? ResolveMount( MountDeclaration declaration, string? topic, string? defaultMountTitle )
                {
                    if ( string.IsNullOrWhiteSpace( declaration.Path ) )
                    {
                        this._writeWarning( $"'{markdownFile}' declares a source node without path." );

                        return null;
                    }

                    var fullPath = Path.GetFullPath( Path.Combine( Path.GetDirectoryName( markdownFile )!, declaration.Path ) ).TrimEnd( '\\', '/' );

                    if ( !Directory.Exists( fullPath ) )
                    {
                        this._writeWarning(
                            $"'{markdownFile}' declares the source directory '{declaration.Path}', but '{fullPath}' does not exist." );

                        return null;
                    }

                    if ( !IsSameOrChildDirectory( rootDirectory, fullPath ) )
                    {
                        this._writeWarning(
                            $"'{markdownFile}' declares the source directory '{declaration.Path}', but it is not under the root '{rootDirectory}'." );

                        return null;
                    }

                    if ( !directories.TryGetValue( fullPath, out var directory ) )
                    {
                        var relativePath = Path.GetRelativePath( rootDirectory, fullPath ).Replace( '\\', '/' );
                        var sitePath = $"{SitePathPrefix}/{root.Name}" + (relativePath == "." ? "" : "/" + relativePath);

                        directory = new DeclaredDirectory( fullPath, sitePath );
                        directories.Add( fullPath, directory );
                    }

                    var children = declaration.Children
                        .Select( c => ResolveMount( c, null, null ) )
                        .OfType<SourceBrowserMount>()
                        .ToList();

                    return new SourceBrowserMount(
                        topic,
                        declaration.Title ?? defaultMountTitle ?? GetTitle( fullPath ),
                        directory.SitePath,
                        children );
                }
            }
        }

        return (directories.Values.ToList(), mounts);
    }

    /// <summary>
    /// Parses the value of the <c>browse-source</c> key: a path, a list of paths, or a list of nodes.
    /// </summary>
    private static IReadOnlyList<MountDeclaration> ParseMountDeclarations( YamlNode node )
    {
        switch ( node )
        {
            case YamlScalarNode { Value: { } path }:
                return [new MountDeclaration( null, path, [] )];

            case YamlSequenceNode sequence:
                return sequence.Children.SelectMany( ParseMountDeclarations ).ToList();

            case YamlMappingNode mapping:
                {
                    string? GetString( string key )
                        => mapping.Children.TryGetValue( new YamlScalarNode( key ), out var value ) && value is YamlScalarNode scalar
                            ? scalar.Value
                            : null;

                    var children = mapping.Children.TryGetValue( new YamlScalarNode( "children" ), out var childrenNode )
                        ? ParseMountDeclarations( childrenNode )
                        : [];

                    return [new MountDeclaration( GetString( "title" ), GetString( "path" ), children )];
                }

            default:
                return [];
        }
    }

    // Matches the code inclusion directives of the Markdig extensions, e.g. [!metalama-file Foo.cs marker="X"].
    [GeneratedRegex( @"\[!metalama-(?:file|files|compare|test)\s+(?<args>[^\]]*)\]" )]
    private static partial Regex InclusionDirectiveRegex();

    // Matches one argument of an inclusion directive. Named arguments (name="value" or name=value) are skipped.
    [GeneratedRegex( @"\w+=""[^""]*""|\w+=\S+|(?<path>\S+)" )]
    private static partial Regex InclusionArgumentRegex();

    /// <summary>
    /// Finds the articles that include each file, by full path of the file.
    /// </summary>
    private Dictionary<string, List<string>> FindReferences()
    {
        var references = new Dictionary<string, List<string>>( StringComparer.OrdinalIgnoreCase );

        var directories = this._roots.Select( r => r.Path ).Concat( this._referenceDirectories )
            .Select( d => Path.GetFullPath( Path.Combine( this._repoDirectory, d ) ) )
            .Where( Directory.Exists )
            .Distinct( StringComparer.OrdinalIgnoreCase );

        foreach ( var directory in directories )
        {
            foreach ( var markdownFile in EnumerateFiles( directory, "*.md" ) )
            {
                var frontMatter = FrontMatterReader.Read( markdownFile );

                if ( frontMatter == null || !frontMatter.TryGetValue( "uid", out var uids ) || uids.Count != 1 )
                {
                    continue;
                }

                var uid = uids[0];
                var markdownDirectory = Path.GetDirectoryName( markdownFile )!;

                foreach ( Match directive in InclusionDirectiveRegex().Matches( File.ReadAllText( markdownFile ) ) )
                {
                    foreach ( Match argument in InclusionArgumentRegex().Matches( directive.Groups["args"].Value ) )
                    {
                        if ( !argument.Groups["path"].Success )
                        {
                            continue;
                        }

                        var path = argument.Groups["path"].Value;

                        // As in PathHelper.ResolvePath, '~' is the root of the git repository of the article.
                        var fullPath = path.StartsWith( '~' )
                            ? Path.Combine( this.GetGitDirectoryOrRepository( markdownFile ), path.TrimStart( '~', '/', '\\' ) )
                            : Path.Combine( markdownDirectory, path );

                        fullPath = Path.GetFullPath( fullPath );

                        if ( !references.TryGetValue( fullPath, out var list ) )
                        {
                            list = [];
                            references.Add( fullPath, list );
                        }

                        if ( !list.Contains( uid ) )
                        {
                            list.Add( uid );
                        }
                    }
                }
            }
        }

        return references;
    }

    private string GetGitDirectoryOrRepository( string path )
    {
        try
        {
            return GitHelper.GetGitDirectory( path );
        }
        catch ( InvalidOperationException )
        {
            return this._repoDirectory;
        }
    }

    private SourceBrowserDirectory GenerateDirectory(
        DeclaredDirectory directory,
        IReadOnlyList<DeclaredDirectory> allDirectories,
        Dictionary<string, List<string>> references )
    {
        var outputDirectory = Path.Combine( this.OutputDirectory, directory.SitePath.Substring( SitePathPrefix.Length + 1 ) );
        var files = new List<SourceBrowserFile>();

        // A subdirectory that is declared as a directory of its own, e.g. a 'Tests' child node, is published by itself,
        // so its files are not listed twice.
        var nestedDirectories = allDirectories
            .Where( d => d != directory && IsSameOrChildDirectory( directory.FullPath, d.FullPath ) )
            .Select( d => d.FullPath )
            .ToHashSet( StringComparer.OrdinalIgnoreCase );

        var sourceFiles = EnumerateFiles( directory.FullPath, "*", nestedDirectories )
            .Where( f => !_excludedSourceExtensions.Contains( Path.GetExtension( f ) ) )

            // Project files are listed first, as in a solution explorer.
            .OrderBy( f => IsProjectFile( f ) ? 0 : 1 )
            .ThenBy( f => f, StringComparer.OrdinalIgnoreCase );

        var sourceFileList = sourceFiles.ToList();
        var sourceFileSet = sourceFileList.ToHashSet( StringComparer.OrdinalIgnoreCase );

        foreach ( var file in sourceFileList )
        {
            var relativePath = Path.GetRelativePath( directory.FullPath, file ).Replace( '\\', '/' );
            var outputFile = Path.Combine( outputDirectory, relativePath );

            Directory.CreateDirectory( Path.GetDirectoryName( outputFile )! );
            File.Copy( file, outputFile, true );

            // In an aspect test, X.t.cs is the expected transformed code of X.cs. It is shown as the transformed code of
            // X.cs, not as a file of its own, but its raw file is published, to be shown when it has no HTML.
            if ( GetTransformedCodeInput( file ) is { } input && sourceFileSet.Contains( input ) )
            {
                continue;
            }

            var transformedFile = Path.ChangeExtension( file, ".t.cs" );

            var transformedPath = file.EndsWith( ".cs", StringComparison.OrdinalIgnoreCase ) && sourceFileSet.Contains( transformedFile )
                ? Path.GetRelativePath( directory.FullPath, transformedFile ).Replace( '\\', '/' )
                : null;

            string? sourceHtml = null;
            string? transformedHtml = null;
            var role = "other";

            if ( file.EndsWith( ".cs", StringComparison.OrdinalIgnoreCase ) )
            {
                var projectDirectory = FindProjectDirectory( file );

                if ( projectDirectory != null )
                {
                    var sourceHtmlPath = PathHelper.GetObjPaths( projectDirectory, file, ".cs.html" ).FirstOrDefault( File.Exists );
                    var transformedHtmlPath = PathHelper.GetObjPaths( projectDirectory, file, ".t.cs.html" ).FirstOrDefault( File.Exists );

                    if ( sourceHtmlPath != null )
                    {
                        sourceHtml = relativePath + ".source.html";
                        File.Copy( sourceHtmlPath, Path.Combine( outputDirectory, sourceHtml ), true );

                        if ( transformedHtmlPath != null && GetComparableCode( sourceHtmlPath ) != GetComparableCode( transformedHtmlPath ) )
                        {
                            transformedHtml = relativePath + ".transformed.html";
                            File.Copy( transformedHtmlPath, Path.Combine( outputDirectory, transformedHtml ), true );
                        }
                    }
                }

                // The same rule as the project buttons.
                if ( File.ReadLines( file ).Any( l => l.StartsWith( "using Metalama.Framework", StringComparison.Ordinal ) ) )
                {
                    role = "aspect";
                }
                else if ( transformedHtml != null || transformedPath != null )
                {
                    role = "target";
                }
            }

            references.TryGetValue( file, out var topics );

            files.Add( new SourceBrowserFile( relativePath, role, this.GetGitHubUrl( file ), sourceHtml, transformedHtml, topics ?? [], transformedPath ) );
        }

        return new SourceBrowserDirectory(
            directory.SitePath,
            GetTitle( directory.FullPath ),
            this.GetGitHubUrl( directory.FullPath ),
            files );
    }

    private static bool IsProjectFile( string path ) => Path.GetExtension( path ).ToLowerInvariant() is ".csproj" or ".sln" or ".slnx";

    /// <summary>
    /// Gets the input file of the expected transformed code of an aspect test, i.e. <c>X.cs</c> for <c>X.t.cs</c>, or
    /// <c>null</c> if the file is not a <c>.t.cs</c> file.
    /// </summary>
    private static string? GetTransformedCodeInput( string file )
        => file.EndsWith( ".t.cs", StringComparison.OrdinalIgnoreCase ) ? file.Substring( 0, file.Length - ".t.cs".Length ) + ".cs" : null;

    private static string GetTitle( string directory )
    {
        var projects = Directory.GetFiles( directory, "*.csproj" );

        return projects.Length == 1 ? Path.GetFileNameWithoutExtension( projects[0] ) : Path.GetFileName( directory );
    }

    /// <summary>
    /// Finds the directory of the project that compiles a file, i.e. the closest parent directory that contains a
    /// project file, without leaving the git repository.
    /// </summary>
    private static string? FindProjectDirectory( string file )
    {
        for ( var directory = Path.GetDirectoryName( file ); directory != null; directory = Path.GetDirectoryName( directory ) )
        {
            if ( Directory.EnumerateFiles( directory, "*.csproj" ).Any() )
            {
                return directory;
            }

            // In a git worktree, .git is a file rather than a directory.
            var gitPath = Path.Combine( directory, ".git" );

            if ( Directory.Exists( gitPath ) || File.Exists( gitPath ) )
            {
                return null;
            }
        }

        return null;
    }

    // Removes the line numbers, the tags and the white space of a syntax-highlighted HTML file, so that the source and
    // transformed code can be compared.
    [GeneratedRegex( @"<span class='line-number'[^>]*>\d+</span>|<[^>]+>|\s+" )]
    private static partial Regex NonCodeRegex();

    private static string GetComparableCode( string htmlPath ) => NonCodeRegex().Replace( File.ReadAllText( htmlPath ), "" );

    private string? GetGitHubUrl( string path )
    {
        string gitDirectory;

        try
        {
            gitDirectory = GitHelper.GetGitDirectory( Path.Combine( path, "_" ) );
        }
        catch ( InvalidOperationException )
        {
            return null;
        }

        if ( !this._gitRepoUrls.TryGetValue( gitDirectory, out var repoUrl ) )
        {
            try
            {
                repoUrl = GitHelper.GetRepoUrl( gitDirectory ) + "/blob/" + GitHelper.GetBranch( gitDirectory );
            }
            catch ( InvalidOperationException e )
            {
                this._writeWarning( $"Cannot get the GitHub URL of '{gitDirectory}': {e.Message}" );
                repoUrl = null;
            }

            this._gitRepoUrls.Add( gitDirectory, repoUrl );
        }

        if ( repoUrl == null )
        {
            return null;
        }

        var relativePath = Path.GetRelativePath( gitDirectory, path ).Replace( '\\', '/' );

        return relativePath == "." ? repoUrl : repoUrl + "/" + relativePath;
    }

    private static bool IsSameOrChildDirectory( string parent, string child )
    {
        var relativePath = Path.GetRelativePath( parent, child );

        return relativePath == "." || (!relativePath.StartsWith( "..", StringComparison.Ordinal ) && !Path.IsPathRooted( relativePath ));
    }

    /// <summary>
    /// Enumerates the files of a directory recursively, skipping build outputs and hidden files and directories.
    /// </summary>
    private static IEnumerable<string> EnumerateFiles( string directory, string pattern, IReadOnlySet<string>? excludedDirectories = null )
    {
        foreach ( var file in Directory.EnumerateFiles( directory, pattern ) )
        {
            var name = Path.GetFileName( file );

            if ( !name.StartsWith( '.' ) && !_excludedExtensions.Contains( Path.GetExtension( name ) ) )
            {
                yield return file;
            }
        }

        foreach ( var subdirectory in Directory.EnumerateDirectories( directory ) )
        {
            var name = Path.GetFileName( subdirectory );

            if ( name.StartsWith( '.' ) || _excludedDirectoryNames.Contains( name ) || excludedDirectories?.Contains( subdirectory ) == true )
            {
                continue;
            }

            // Skip symbolic links and junctions, which could create cycles.
            if ( new DirectoryInfo( subdirectory ).LinkTarget != null )
            {
                continue;
            }

            foreach ( var file in EnumerateFiles( subdirectory, pattern, excludedDirectories ) )
            {
                yield return file;
            }
        }
    }
}
