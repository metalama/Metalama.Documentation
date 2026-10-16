// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Docfx.MarkdigEngine.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BuildMetalamaDocumentation.SourceBrowser;

/// <summary>
/// The content of the index file written by <see cref="SourceBrowserGenerator"/>.
/// </summary>
internal sealed record SourceBrowserIndexData( IReadOnlyList<SourceBrowserIndexEntry> Directories );

/// <summary>
/// Maps a local source directory to its path in the site.
/// </summary>
internal sealed record SourceBrowserIndexEntry( string LocalPath, string SitePath );

/// <summary>
/// Computes the links from articles to the source browser, for the Markdig extensions. The links are relative to the
/// HTML page of the article, as DocFx writes it, so that HelpBrowser resolves them like any other relative link.
/// </summary>
internal static class SourceBrowserLinks
{
    private static readonly object _sync = new();
    private static string? _loadedRepoDirectory;
    private static IReadOnlyList<SourceBrowserIndexEntry> _entries = [];
    private static IReadOnlyList<(string Source, string Destination)> _contentMappings = [];

    /// <summary>
    /// Gets the link from the article being rendered to a local file or directory, or <c>null</c> if the file or
    /// directory is not published in the source browser.
    /// </summary>
    public static string? GetHref( string localPath )
    {
        var repoDirectory = Environment.CurrentDirectory;
        var articlePath = InclusionContext.File?.ToString();

        if ( articlePath == null )
        {
            return null;
        }

        return GetHref( repoDirectory, articlePath, localPath );
    }

    /// <summary>
    /// Gets the link from an article, given by its path relative to the repository, to a local file or directory.
    /// </summary>
    public static string? GetHref( string repoDirectory, string articlePath, string localPath )
    {
        EnsureLoaded( repoDirectory );

        var sitePath = GetSitePath( localPath );

        if ( sitePath == null )
        {
            return null;
        }

        var articleSitePath = GetArticleSitePath( articlePath );

        if ( articleSitePath == null )
        {
            return null;
        }

        var baseUri = new Uri( "https://site/" + articleSitePath );
        var targetUri = new Uri( "https://site/" + sitePath );

        return Uri.UnescapeDataString( baseUri.MakeRelativeUri( targetUri ).ToString() );
    }

    private static string? GetSitePath( string localPath )
    {
        localPath = Path.GetFullPath( localPath ).TrimEnd( '\\', '/' );

        foreach ( var entry in _entries )
        {
            var relativePath = Path.GetRelativePath( entry.LocalPath, localPath );

            if ( relativePath == "." )
            {
                return entry.SitePath;
            }
            else if ( !relativePath.StartsWith( "..", StringComparison.Ordinal ) && !Path.IsPathRooted( relativePath ) )
            {
                return entry.SitePath + "/" + relativePath.Replace( '\\', '/' );
            }
        }

        return null;
    }

    /// <summary>
    /// Gets the path of the HTML page that DocFx generates for an article, according to the content mappings of
    /// <c>docfx.json</c>.
    /// </summary>
    private static string? GetArticleSitePath( string articlePath )
    {
        articlePath = articlePath.Replace( '\\', '/' ).TrimStart( '/' );

        foreach ( var (source, destination) in _contentMappings )
        {
            var prefix = source.Length == 0 ? "" : source + "/";

            if ( articlePath.StartsWith( prefix, StringComparison.OrdinalIgnoreCase ) )
            {
                var relativePath = Path.ChangeExtension( articlePath.Substring( prefix.Length ), ".html" );

                return destination.Length == 0 ? relativePath : destination + "/" + relativePath;
            }
        }

        return null;
    }

    private static void EnsureLoaded( string repoDirectory )
    {
        lock ( _sync )
        {
            if ( _loadedRepoDirectory == repoDirectory )
            {
                return;
            }

            _loadedRepoDirectory = repoDirectory;

            var indexFile = SourceBrowserGenerator.GetIndexFile( repoDirectory );

            _entries = File.Exists( indexFile )
                ? JsonSerializer.Deserialize<SourceBrowserIndexData>(
                      File.ReadAllText( indexFile ),
                      new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase } )
                  ?.Directories ?? []
                : [];

            // A directory can be inside another one, e.g. a 'Tests' child node; the most specific directory wins.
            _entries = _entries.OrderByDescending( e => e.LocalPath.Length ).ToList();

            _contentMappings = ReadContentMappings( Path.Combine( repoDirectory, "docfx.json" ) );
        }
    }

    private static IReadOnlyList<(string Source, string Destination)> ReadContentMappings( string docfxJsonPath )
    {
        if ( !File.Exists( docfxJsonPath ) )
        {
            return [];
        }

        using var document = JsonDocument.Parse(
            File.ReadAllText( docfxJsonPath ),
            new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip, AllowTrailingCommas = true } );

        if ( !document.RootElement.TryGetProperty( "build", out var build ) || !build.TryGetProperty( "content", out var content ) )
        {
            return [];
        }

        static string GetString( JsonElement element, string name )
            => element.TryGetProperty( name, out var value ) ? value.GetString()?.Replace( '\\', '/' ).Trim( '/' ) ?? "" : "";

        return content.EnumerateArray()
            .Select( c => (GetString( c, "src" ), GetString( c, "dest" )) )

            // Test the longest source paths first, because a mapping with an empty source matches every article.
            .OrderByDescending( m => m.Item1.Length )
            .ToList();
    }
}
