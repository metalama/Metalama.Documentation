// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using System;

namespace BuildMetalamaDocumentation.Markdig.ProjectButtons;

public class ProjectButtonsInlineParser : InlineParser
{
    private const string _startString = "[!metalama-project-buttons";

    public ProjectButtonsInlineParser()
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

        var buttons = new ProjectButtonsInline() { Directory = resolvedPath };

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
                    buttons.Name = value;

                    break;

                case "title":
                    buttons.Title = value;

                    break;

                default:
                    throw new InvalidOperationException( $"Unknown argument '{value}'." );
            }
        }

        slice.EnsureClosingBracket();

        buttons.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        buttons.Line = line;
        buttons.Column = column;

        processor.Inline = buttons;

        return true;
    }
}