// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Markdig.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BuildMetalamaDocumentation.Markdig.Helpers;

public static class StringSliceExtensions
{
    public static void SkipWhitespaces( this ref StringSlice slice )
    {
        while ( slice.CurrentChar.IsWhitespace() )
        {
            slice.SkipChar();
        }
    }

    public static string ReadUntilCharOrWhitespace( this ref StringSlice slice, char endChar ) => slice.ReadUntil( c => c == endChar || c.IsWhitespace() );

    public static string ReadUntilChar( this ref StringSlice slice, char endChar ) => slice.ReadUntil( c => c == endChar );

    public static string ReadUntil(
        this ref StringSlice slice,
        Func<char, bool> predicate )
    {
        var builder = StringBuilderCache.Local();

        var c = slice.CurrentChar;

        while ( !predicate( c ) && c != '\0' )
        {
            builder.Append( c );
            c = slice.NextChar();
        }

        return builder.ToString();
    }

    public static bool MatchArgument( this ref StringSlice slice, out string? name, out string? value )
    {
        slice.SkipWhitespaces();

        name = slice.ReadUntil( c => c == '=' || c == ']' || c.IsWhitespace() );

        value = null;

        if ( name.Length == 0 )
        {
            name = null;

            return false;
        }

        slice.SkipWhitespaces();

        if ( slice.CurrentChar == '=' )
        {
            slice.SkipChar();

            var isQuoted = slice.CurrentChar == '"';

            if ( isQuoted )
            {
                slice.SkipChar(); // Opening quote.

                value = slice.ReadUntilChar( '"' );

                if ( slice.CurrentChar != '"' )
                {
                    throw new InvalidOperationException( "Quoted value is missing closing quote." );
                }

                slice.SkipChar(); // End quote.
            }
            else
            {
                value = slice.ReadUntilCharOrWhitespace( ']' );
            }
        }
        else
        {
            value = name;
            name = null;
        }

        if ( string.IsNullOrEmpty( name ) && string.IsNullOrEmpty( value ) )
        {
            throw new InvalidOperationException( "Argument has no name and value." );
        }

        return true;
    }

    public static bool MatchArgument( this ref StringSlice slice, [NotNullWhen( true )] out string? value )
    {
        if ( !slice.MatchArgument( out var name, out value ) )
        {
            return false;
        }

        if ( !string.IsNullOrEmpty( name ) )
        {
            throw new InvalidOperationException( $"Unexpected named argument '{name}'." );
        }

        if ( string.IsNullOrEmpty( value ) )
        {
            throw new InvalidOperationException( $"Unnamed argument has no value." );
        }

        return true;
    }

    public static void EnsureClosingBracket( this ref StringSlice slice )
    {
        slice.SkipWhitespaces();

        if ( slice.CurrentChar != ']' )
        {
            throw new InvalidOperationException( "Expected closing bracket ']'." );
        }

        slice.SkipChar();
    }
}