// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace BuildMetalamaDocumentation.SourceBrowser;

/// <summary>
/// Reads the YAML front matter of a Markdown file, i.e. the block between the first two <c>---</c> lines.
/// </summary>
internal static class FrontMatterReader
{
    /// <summary>
    /// Reads the front matter of a Markdown file as a list of strings by key. Returns <c>null</c> when the file has no
    /// front matter. A scalar value gives a list of one item, and a sequence of scalars gives one item per element.
    /// Other values, e.g. mappings, are skipped; use <see cref="ReadMapping"/> to read them.
    /// </summary>
    public static IReadOnlyDictionary<string, IReadOnlyList<string>>? Read( string path )
    {
        var mapping = ReadMapping( path );

        if ( mapping == null )
        {
            return null;
        }

        var result = new Dictionary<string, IReadOnlyList<string>>( StringComparer.Ordinal );

        foreach ( var (keyNode, valueNode) in mapping.Children )
        {
            if ( keyNode is not YamlScalarNode { Value: { } key } )
            {
                continue;
            }

            switch ( valueNode )
            {
                case YamlScalarNode { Value: { } value }:
                    result[key] = [value];

                    break;

                case YamlSequenceNode sequence:
                    result[key] = sequence.Children.OfType<YamlScalarNode>()
                        .Select( n => n.Value )
                        .OfType<string>()
                        .ToList();

                    break;
            }
        }

        return result;
    }

    /// <summary>
    /// Reads the front matter of a Markdown file as a YAML mapping. Returns <c>null</c> when the file has no front matter.
    /// </summary>
    public static YamlMappingNode? ReadMapping( string path )
    {
        using var reader = new StreamReader( path );

        var firstLine = reader.ReadLine();

        if ( firstLine == null || firstLine.TrimEnd() != "---" )
        {
            return null;
        }

        var yaml = new StringBuilder();

        while ( reader.ReadLine() is { } line )
        {
            if ( line.TrimEnd() == "---" )
            {
                var stream = new YamlStream();
                stream.Load( new StringReader( yaml.ToString() ) );

                return stream.Documents.Count > 0 && stream.Documents[0].RootNode is YamlMappingNode mapping
                    ? mapping
                    : new YamlMappingNode();
            }

            yaml.AppendLine( line );
        }

        // The front matter is not closed.
        return null;
    }
}
