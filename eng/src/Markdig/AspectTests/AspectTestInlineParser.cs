// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation.Markdig.AspectTests;

public class AspectTestInlineParser : InlineParser
{
    private const string _startString = "[!metalama-test";

    public AspectTestInlineParser()
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

        var test = new AspectTestInline { Src = resolvedPath };

        while ( slice.MatchArgument( out var name, out var value ) )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                throw new InvalidOperationException( $"Unexpected unnamed argument '{value}'." );
            }

            if ( string.IsNullOrEmpty( value ) )
            {
                throw new InvalidOperationException( $"Argument '{name}' is missing a value." );
            }

            switch ( name )
            {
                case "name":
                    test.Name = value;

                    break;

                case "title":
                    test.Title = value;

                    break;

                case "tabs":
                    test.Tabs = TabsHelper.SplitTabs( value );

                    break;

                case "diff-side":
                    test.DiffSide = Enum.Parse<DiffSide>( value, true );

                    break;

                case "diff-files":
                    test.DiffFilesPattern = new Regex( value, RegexOptions.IgnoreCase );

                    break;

                default:
                    throw new InvalidOperationException( $"Unknown argument '{name}'." );
            }
        }

        slice.EnsureClosingBracket();

        test.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        test.Line = line;
        test.Column = column;

        processor.Inline = test;

        return true;
    }
}