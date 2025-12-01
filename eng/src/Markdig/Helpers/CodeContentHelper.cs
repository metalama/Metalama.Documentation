// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation.Markdig.Helpers;

internal static class CodeContentHelper
{
    private static readonly Regex _snippetStartRegex = new( @"//\s*\[<snippet\s+(\w+)\s*>\]", RegexOptions.Compiled );
    private static readonly Regex _snippetEndRegex = new( @"//\s*\[<endsnippet\s+(\w+)\s*>\]", RegexOptions.Compiled );

    /// <summary>
    /// Processes code content by removing leading comments and trimming empty lines.
    /// </summary>
    public static string ProcessCodeContent( string[] lines )
    {
        var result = new List<string>();
        var skipLeadingComments = true;

        foreach ( var line in lines )
        {
            if ( skipLeadingComments && line.TrimStart().StartsWith( "//", StringComparison.Ordinal ) )
            {
                continue;
            }

            skipLeadingComments = false;

            // Skip snippet markers in output.
            if ( _snippetStartRegex.IsMatch( line ) || _snippetEndRegex.IsMatch( line ) )
            {
                continue;
            }

            result.Add( line );
        }

        // Trim empty lines at start and end.
        while ( result.Count > 0 && string.IsNullOrWhiteSpace( result[0] ) )
        {
            result.RemoveAt( 0 );
        }

        while ( result.Count > 0 && string.IsNullOrWhiteSpace( result[^1] ) )
        {
            result.RemoveAt( result.Count - 1 );
        }

        return string.Join( Environment.NewLine, result );
    }

    /// <summary>
    /// Extracts a marked snippet from code lines.
    /// </summary>
    public static string? ExtractSnippet( string[] lines, string marker )
    {
        var result = new List<string>();
        var capturing = false;
        var foundStart = false;
        var foundEnd = false;

        foreach ( var line in lines )
        {
            var startMatch = _snippetStartRegex.Match( line );

            if ( startMatch.Success && startMatch.Groups[1].Value == marker )
            {
                capturing = true;
                foundStart = true;

                continue;
            }

            var endMatch = _snippetEndRegex.Match( line );

            if ( endMatch.Success && endMatch.Groups[1].Value == marker )
            {
                capturing = false;
                foundEnd = true;

                continue;
            }

            if ( capturing )
            {
                // Skip nested snippet markers.
                if ( _snippetStartRegex.IsMatch( line ) || _snippetEndRegex.IsMatch( line ) )
                {
                    continue;
                }

                result.Add( line );
            }
        }

        if ( !foundStart || !foundEnd )
        {
            return null;
        }

        // Trim empty lines.
        while ( result.Count > 0 && string.IsNullOrWhiteSpace( result[0] ) )
        {
            result.RemoveAt( 0 );
        }

        while ( result.Count > 0 && string.IsNullOrWhiteSpace( result[^1] ) )
        {
            result.RemoveAt( result.Count - 1 );
        }

        // Calculate minimum indentation.
        var minIndent = int.MaxValue;

        foreach ( var line in result )
        {
            if ( string.IsNullOrWhiteSpace( line ) )
            {
                continue;
            }

            var indent = line.Length - line.TrimStart().Length;
            minIndent = Math.Min( minIndent, indent );
        }

        // Remove common indentation.
        if ( minIndent > 0 && minIndent != int.MaxValue )
        {
            for ( var i = 0; i < result.Count; i++ )
            {
                if ( result[i].Length >= minIndent )
                {
                    result[i] = result[i].Substring( minIndent );
                }
            }
        }

        return string.Join( Environment.NewLine, result );
    }
}
