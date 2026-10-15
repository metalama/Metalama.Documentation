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
    /// Reads the front matter of a Markdown file. Returns <c>null</c> when the file has no front matter.
    /// A scalar value gives a list of one item, and a sequence gives one item per element.
    /// </summary>
    public static IReadOnlyDictionary<string, IReadOnlyList<string>>? Read( string path )
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
                return Parse( yaml.ToString() );
            }

            yaml.AppendLine( line );
        }

        // The front matter is not closed.
        return null;
    }

    private static IReadOnlyDictionary<string, IReadOnlyList<string>> Parse( string yaml )
    {
        var result = new Dictionary<string, IReadOnlyList<string>>( StringComparer.Ordinal );

        var stream = new YamlStream();
        stream.Load( new StringReader( yaml ) );

        if ( stream.Documents.Count == 0 || stream.Documents[0].RootNode is not YamlMappingNode mapping )
        {
            return result;
        }

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
}
