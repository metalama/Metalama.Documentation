﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

public sealed class CompareFileTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new( @"^\s*\[!metalama-compare +(?<path>[^\s\]]+)\s*\]" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = _regex.Match( context.CurrentMarkdown );

        if ( match.Success )
        {
            var sourceInfo = context.Consume( match.Length );

            var path = match.Groups["path"].Value;

            return new CompareFileToken( this, parser.Context, sourceInfo, path );
        }

        return null;
    }

    public string Name => "Metalama.SingleFile";
}