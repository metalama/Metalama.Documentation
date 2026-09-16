// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BuildMetalamaDocumentation.Markdig.MultipleFiles;

public class MultipleFilesInlineParser : InlineParser
{
    private const string _startString = "[!metalama-files";

    public MultipleFilesInlineParser()
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

        var files = new MultipleFilesInline();
        var filesList = new List<string>();
        string? name;
        string? value;

        while ( slice.MatchArgument( out name, out value ) )
        {
            if ( !string.IsNullOrEmpty( name ) )
            {
                break;
            }

            if ( string.IsNullOrEmpty( value ) )
            {
                throw new InvalidOperationException( "Unnamed argument has no value." );
            }

            var resolvedPath = PathHelper.ResolvePath( value );
            filesList.Add( resolvedPath );
        }

        while ( name != null )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new InvalidOperationException( $"Argument '{name}' is missing a value." );
            }

            switch ( name )
            {
                case "name":
                    files.Name = value;

                    break;

                case "mode":
                    files.Mode = Enum.Parse<TabMode>( value );

                    break;

                case "links":
                    files.AddLinks = XmlConvert.ToBoolean( value );

                    break;

                default:
                    throw new InvalidOperationException( $"Unknown argument '{name}'." );
            }

            if ( !slice.MatchArgument( out name, out value ) )
            {
                break;
            }

            if ( string.IsNullOrEmpty( name ) )
            {
                throw new InvalidOperationException( $"Unexpected unnamed argument '{value}'." );
            }
        }

        slice.EnsureClosingBracket();

        files.Files = filesList.ToArray();

        files.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        files.Line = line;
        files.Column = column;

        processor.Inline = files;

        return true;
    }
}