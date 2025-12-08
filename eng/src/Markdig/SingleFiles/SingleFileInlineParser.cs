// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using System;

namespace BuildMetalamaDocumentation.Markdig.SingleFiles;

public class SingleFileInlineParser : InlineParser
{
    private const string _startString = "[!metalama-file";

    public SingleFileInlineParser()
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

        var file = new SingleFileInline { Src = resolvedPath };

        while ( slice.MatchArgument( out var name, out var value ) )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                if ( value == "transformed" )
                {
                    file.ShowTransformed = true;

                    continue;
                }

                if ( value == "diff" )
                {
                    file.Diff = true;

                    continue;
                }

                throw new InvalidOperationException( $"Unexpected unnamed argument '{value}'." );
            }

            if ( string.IsNullOrEmpty( value ) )
            {
                throw new InvalidOperationException( $"Argument '{name}' is missing a value." );
            }

            switch ( name )
            {
                case "name":
                    file.Name = value;

                    break;

                case "title":
                    file.Title = value;

                    break;

                case "tabs":
                    file.Tabs = TabsHelper.SplitTabs( value );

                    break;

                case "marker":
                    file.Marker = value;

                    break;

                case "member":
                    file.Member = value;

                    break;

                case "transformed":
                    file.ShowTransformed = bool.Parse( value );

                    break;

                case "diff":
                    file.Diff = bool.Parse( value );

                    break;

                // TODO
                case "from":
                    break;

                default:
                    throw new InvalidOperationException( $"Unknown argument '{name}'." );
            }
        }

        slice.EnsureClosingBracket();

        file.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        file.Line = line;
        file.Column = column;

        processor.Inline = file;

        return true;
    }
}