﻿// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System.Linq;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SingleFileTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new(
        @"^\s*\[!metalama-file +(?<path>[^\s\]]+)\s*(?<transformed>transformed)?\s*(?<attributes>[^\]]*)\]" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = _regex.Match( context.CurrentMarkdown );

        if ( match.Success )
        {
            var sourceInfo = context.Consume( match.Length );

            var path = match.Groups["path"].Value;
            var showTransformed = match.Groups["transformed"].Success;

            var attributes = AttributeMatcher.ParseAttributes( match.Groups["attributes"].Value );

            attributes.TryGetValue( "marker", out var marker );
            attributes.TryGetValue( "member", out var member );

            return new SingleFileToken(
                this,
                parser.Context,
                sourceInfo,
                path,
                showTransformed,
                marker,
                member?.Trim() );
        }

        return null;
    }

    public string Name => "Metalama.SingleFile";
}