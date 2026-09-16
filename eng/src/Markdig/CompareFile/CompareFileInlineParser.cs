// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using System;

namespace BuildMetalamaDocumentation.Markdig.CompareFile;

internal class CompareFileInlineParser : InlineParser
{
    private const string _startString = "[!metalama-compare";

    public CompareFileInlineParser()
    {
        this.OpeningCharacters = ['['];
    }

    public override bool Match( InlineProcessor processor, ref StringSlice slice )
    {
        var saved = slice;

        if ( !ExtensionsHelper.MatchStart( ref slice, _startString, false ) )
        {
            return false;
        }

        if ( !slice.MatchArgument( out var path ) )
        {
            throw new InvalidOperationException( $"Path is missing for '{_startString}'" );
        }

        var resolvedPath = PathHelper.ResolvePath( path );

        var diff = new CompareFileInline { Src = resolvedPath };

        slice.EnsureClosingBracket();

        diff.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        diff.Line = line;
        diff.Column = column;

        processor.Inline = diff;

        return true;
    }
}