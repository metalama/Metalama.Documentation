// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.AspectTests;
using BuildMetalamaDocumentation.Markdig.MultipleFiles;
using BuildMetalamaDocumentation.Markdig.SingleFiles;
using Docfx.Common;
using Docfx.MarkdigEngine.Extensions;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using PostSharp.Engineering.BuildTools.Utilities;

namespace BuildMetalamaDocumentation.UnitTests;

public class ParserTests
{
    private static void Test<TMarkdownExtension, TInline>( string text, bool isMatch, bool throws, string? path, Action<TInline, string?> assert )
        where TMarkdownExtension : class, IMarkdownExtension, new()
        where TInline : Inline
    {
        var pipelineBuilder = new MarkdownPipelineBuilder();
        pipelineBuilder.UseAdvancedExtensions();
        pipelineBuilder.Extensions.AddIfNotAlready<TMarkdownExtension>();

        var pipeline = pipelineBuilder.Build();

        InclusionContext.PushFile( "test.md" );

        if ( throws )
        {
            Assert.Throws<InvalidOperationException>( () => _ = Markdown.Parse( text, pipeline ) );

            return;
        }

        var document = Markdown.Parse( text, pipeline );

        if ( ((ParagraphBlock) document[0]).Inline?.FirstChild is not TInline inline )
        {
            Assert.False( isMatch );

            return;
        }

        Assert.True( isMatch );

        string? expectedPath;

        if ( path == null )
        {
            expectedPath = null;
        }
        else
        {
            var rootDirectory = text.Contains( '~', StringComparison.Ordinal )
                ? BuildMetalamaDocumentation.Markdig.Helpers.GitHelper.GetGitDirectory( Environment.CurrentDirectory )
                : Environment.CurrentDirectory;

            expectedPath = Path.Combine( rootDirectory, path );
        }

        assert( inline, expectedPath );
    }

    [Theory]
    [InlineData( "not a match", false )]
    [InlineData( "[!metalama-test some/path/to/somewhere.cs]", true, "some\\path\\to\\somewhere.cs" )]
    [InlineData( "[!metalama-test ~/some/path/to/somewhere.cs]", true, "some\\path\\to\\somewhere.cs" )]
    [InlineData( "[!metalama-test ~/some/path/to/somewhere.cs name=\"the name\"]", true, "some\\path\\to\\somewhere.cs", "the name" )]
    [InlineData(
        "[!metalama-test ~/some/path/to/somewhere.cs name=\"the name\" title=\"the title\"]",
        true,
        "some\\path\\to\\somewhere.cs",
        "the name",
        "the title" )]
    public void AspectTest( string text, bool isMatch, string? path = null, string name = "", string title = "" )
        => Test<AspectTestInlineExtension, AspectTestInline>(
            text,
            isMatch,
            false,
            path,
            ( 
                
                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                inline,
                
                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                expectedPath ) =>
            {
                Assert.NotNull( expectedPath );
                Assert.Equal( expectedPath, inline.Src );
                Assert.Equal( name, inline.Name );
                Assert.Equal( title, inline.Title );
            } );

    [Theory]
    [InlineData( "not a match", false )]
    [InlineData( "[!metalama-file some/path.cs]", true, "some\\path.cs", false )]
    [InlineData( "[!metalama-file some/path.cs transformed ]", true, "some\\path.cs", true )]
    public void SingleFile( string text, bool isMatch, string? path = null, bool showTransformed = false )
        => Test<SingleFileInlineExtension, SingleFileInline>(
            text,
            isMatch,
            false,
            path,
            ( 
                
                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                inline,
                
                // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                expectedPath ) =>
            {
                Assert.NotNull( expectedPath );
                Assert.Equal( expectedPath, inline.Src );
                Assert.Equal( showTransformed, inline.ShowTransformed );
            } );

    [Theory]
    [InlineData( "not a match", false, false, 0 )]
    [InlineData( "[!metalama-files a.cs]", true, false, 1 )]
    [InlineData( "[!metalama-files a.cs b.cs]", true, false, 2 )]
    [InlineData( "[!metalama-files a.cs ]", true, false, 1 )]
    [InlineData( "[!metalama-files a.cs b.cs ]", true, false, 2 )]
    [InlineData( "[!metalama-files a.cs b.cs a=\"b\"]", true, true, 2 )]
    [InlineData( "[!metalama-files a.cs b.cs a=\"b\" ]", true, true, 2 )]
    public void MultipleFiles( string text, bool isMatch, bool throws, int files )
        => Test<MultipleFilesInlineExtension, MultipleFilesInline>(
            text,
            isMatch,
            throws,
            "",

            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            ( inline, _ ) =>
            {
                Assert.Equal( files, inline.Files.Length );
            } );
}