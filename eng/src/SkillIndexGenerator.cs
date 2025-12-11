// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildMetalamaDocumentation;

/// <summary>
/// Generates the index.yml file for the Claude SKILL by parsing Markdown front matter
/// and mirroring the toc.yml hierarchy.
/// </summary>
internal class SkillIndexGenerator
{
    private readonly string _repoDir;
    private readonly ConsoleHelper _console;
    private readonly Dictionary<string, MarkdownMetadata> _metadataByUid = new();
    private readonly Dictionary<string, string> _pathByUid = new();

    public SkillIndexGenerator( string repoDir, ConsoleHelper console )
    {
        _repoDir = repoDir;
        _console = console;
    }

    public string GenerateIndex()
    {
        // Step 1: Scan all Markdown files and extract front matter
        ScanMarkdownFiles();

        // Step 2: Parse the main toc.yml and build hierarchical structure
        var tocPath = Path.Combine( _repoDir, "content", "toc.yml" );
        var indexItems = ParseTocFile( tocPath, "content" );

        // Step 3: Serialize to YAML
        var serializer = new SerializerBuilder()
            .WithNamingConvention( CamelCaseNamingConvention.Instance )
            .ConfigureDefaultValuesHandling( DefaultValuesHandling.OmitNull )
            .Build();

        return serializer.Serialize( indexItems );
    }

    private void ScanMarkdownFiles()
    {
        var contentDir = Path.Combine( _repoDir, "content" );

        if ( !Directory.Exists( contentDir ) )
        {
            return;
        }

        foreach ( var file in Directory.GetFiles( contentDir, "*.md", SearchOption.AllDirectories ) )
        {
            var metadata = ParseMarkdownFrontMatter( file );

            if ( metadata != null && !string.IsNullOrEmpty( metadata.Uid ) )
            {
                var relativePath = Path.GetRelativePath( _repoDir, file ).Replace( '\\', '/' );
                _metadataByUid[metadata.Uid] = metadata;
                _pathByUid[metadata.Uid] = relativePath;
            }
        }

        _console.WriteMessage( $"Scanned {_metadataByUid.Count} Markdown files with UIDs" );
    }

    private MarkdownMetadata? ParseMarkdownFrontMatter( string filePath )
    {
        try
        {
            var content = File.ReadAllText( filePath );

            // Check for YAML front matter (starts with ---)
            if ( !content.StartsWith( "---", StringComparison.Ordinal ) )
            {
                return null;
            }

            var endIndex = content.IndexOf( "---", 3, StringComparison.Ordinal );

            if ( endIndex < 0 )
            {
                return null;
            }

            var frontMatter = content.Substring( 3, endIndex - 3 ).Trim();

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention( CamelCaseNamingConvention.Instance )
                .IgnoreUnmatchedProperties()
                .Build();

            return deserializer.Deserialize<MarkdownMetadata>( frontMatter );
        }
        catch
        {
            return null;
        }
    }

    private List<IndexItem> ParseTocFile( string tocPath, string baseDir )
    {
        if ( !File.Exists( tocPath ) )
        {
            return new List<IndexItem>();
        }

        try
        {
            var content = File.ReadAllText( tocPath );

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention( CamelCaseNamingConvention.Instance )
                .IgnoreUnmatchedProperties()
                .Build();

            var tocRoot = deserializer.Deserialize<TocRoot>( content );

            if ( tocRoot?.Items == null )
            {
                return new List<IndexItem>();
            }

            return ConvertTocItems( tocRoot.Items, baseDir, Path.GetDirectoryName( tocPath )! );
        }
        catch ( Exception ex )
        {
            _console.WriteWarning( $"Failed to parse {tocPath}: {ex.Message}" );

            return new List<IndexItem>();
        }
    }

    private List<IndexItem> ConvertTocItems( List<TocItem> tocItems, string baseDir, string currentDir )
    {
        var result = new List<IndexItem>();

        foreach ( var tocItem in tocItems )
        {
            var indexItem = new IndexItem { Name = tocItem.Name };

            // If there's a topicUid, look up the metadata
            if ( !string.IsNullOrEmpty( tocItem.TopicUid ) )
            {
                if ( _pathByUid.TryGetValue( tocItem.TopicUid, out var path ) )
                {
                    indexItem.Path = path;
                }

                if ( _metadataByUid.TryGetValue( tocItem.TopicUid, out var metadata ) )
                {
                    indexItem.Summary = metadata.Summary;
                    indexItem.Keywords = metadata.Keywords;
                }
            }

            // If there's an href to another toc.yml, recurse into it
            if ( !string.IsNullOrEmpty( tocItem.Href ) && tocItem.Href.EndsWith( "toc.yml", StringComparison.Ordinal ) )
            {
                var subTocPath = Path.Combine( currentDir, tocItem.Href.Replace( '/', Path.DirectorySeparatorChar ) );
                var subTocDir = Path.GetDirectoryName( subTocPath )!;
                var subItems = ParseTocFile( subTocPath, baseDir );

                if ( subItems.Count > 0 )
                {
                    indexItem.Items = subItems;
                }
            }

            // If there are nested items in the current toc
            if ( tocItem.Items != null && tocItem.Items.Count > 0 )
            {
                var childItems = ConvertTocItems( tocItem.Items, baseDir, currentDir );

                if ( childItems.Count > 0 )
                {
                    indexItem.Items = indexItem.Items != null
                        ? indexItem.Items.Concat( childItems ).ToList()
                        : childItems;
                }
            }

            result.Add( indexItem );
        }

        return result;
    }

    // Classes for YAML deserialization
    private class MarkdownMetadata
    {
        public string? Uid { get; set; }

        public string? Summary { get; set; }

        public string? Keywords { get; set; }

        public int? Level { get; set; }
    }

    private class TocRoot
    {
        public List<TocItem>? Items { get; set; }
    }

    private class TocItem
    {
        public string? Name { get; set; }

        public string? TopicUid { get; set; }

        public string? Href { get; set; }

        public List<TocItem>? Items { get; set; }
    }

    // Class for YAML serialization of output
    private class IndexItem
    {
        public string? Name { get; set; }

        public string? Path { get; set; }

        public string? Summary { get; set; }

        public string? Keywords { get; set; }

        public List<IndexItem>? Items { get; set; }
    }
}
