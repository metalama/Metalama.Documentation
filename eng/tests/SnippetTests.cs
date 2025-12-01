// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;

namespace BuildMetalamaDocumentation.UnitTests;

public class SnippetTests
{
    [Fact]
    public void TestIndentation()
    {
        var htmlFullPath = Path.Combine( Path.GetDirectoryName( this.GetType().Assembly.Location! )!, "GenerateBuilderAttribute.cs.html" );
        var tab = new CodeTab( "Test", htmlFullPath, SandboxFileKind.ExtraCode, member: "GenerateBuilderAttribute.BuildAspect", htmlPath: htmlFullPath );

        var snippet = tab.GetTabContent();

        Assert.NotNull( snippet );
        Assert.Contains( "BuildAspect", snippet );
    }

    [Fact]
    public void TabGroup_RendersJsonLd()
    {
        // Arrange: create a temp file with test code.
        var tempFile = Path.Combine( Path.GetTempPath(), "TestCode.cs" );

        try
        {
            File.WriteAllText( tempFile, @"// Copyright notice
using System;

public class TestClass
{
    public void TestMethod() { }
}" );

            var tabGroup = new TestTabGroup( "test-group" );
            tabGroup.Tabs.Add( new CodeTab( "TestCode", tempFile, SandboxFileKind.ExtraCode, htmlPath: tempFile ) );

            var writer = new StringWriter();
            var renderer = new HtmlRenderer( writer );
            var inline = new TestTabGroupInline { AddLinks = false };

            // Act
            tabGroup.Render( renderer, inline );
            writer.Flush();
            var html = writer.ToString();

            // Assert: Check for JSON-LD block.
            Assert.Contains( "application/ld+json", html );
            Assert.Contains( "SoftwareSourceCode", html );

            // Extract the JSON-LD content to verify leading comments are stripped.
            var jsonLdStart = html.IndexOf( "<script type=\"application/ld+json\">", StringComparison.Ordinal );
            var jsonLdEnd = html.IndexOf( "</script>", jsonLdStart, StringComparison.Ordinal );
            var jsonLdContent = html.Substring( jsonLdStart, jsonLdEnd - jsonLdStart + "</script>".Length );

            Assert.Contains( "public class TestClass", jsonLdContent );
            Assert.DoesNotContain( "Copyright notice", jsonLdContent );
        }
        finally
        {
            if ( File.Exists( tempFile ) )
            {
                File.Delete( tempFile );
            }
        }
    }

    private class TestTabGroup : TabGroup
    {
        public TestTabGroup( string tabGroupId ) : base( tabGroupId ) { }

        public override string GetGitUrl() => "https://example.com";
    }

    private class TestTabGroupInline : TabGroupBaseInline { }

    [Fact]
    public void ProcessCodeContent_SkipsLeadingComments()
    {
        var lines = new[]
        {
            "// Copyright notice",
            "// Another comment",
            "",
            "using System;",
            "",
            "public class Test { }"
        };

        var result = CodeContentHelper.ProcessCodeContent( lines );

        Assert.DoesNotContain( "Copyright", result );
        Assert.Contains( "using System;", result );
        Assert.Contains( "public class Test", result );
    }

    [Fact]
    public void ProcessCodeContent_SkipsSnippetMarkers()
    {
        var lines = new[]
        {
            "using System;",
            "",
            "// [<snippet Test>]",
            "public class Test { }",
            "// [<endsnippet Test>]"
        };

        var result = CodeContentHelper.ProcessCodeContent( lines );

        Assert.Contains( "public class Test", result );
        Assert.DoesNotContain( "[<snippet", result );
        Assert.DoesNotContain( "[<endsnippet", result );
    }

    [Fact]
    public void ProcessCodeContent_TrimsEmptyLines()
    {
        var lines = new[]
        {
            "",
            "",
            "public class Test { }",
            "",
            ""
        };

        var result = CodeContentHelper.ProcessCodeContent( lines );

        Assert.Equal( "public class Test { }", result );
    }

    [Fact]
    public void ExtractSnippet_ReturnsCorrectContent()
    {
        var lines = new[]
        {
            "using System;",
            "",
            "public class Outer",
            "{",
            "    // [<snippet MySnippet>]",
            "    public void Method()",
            "    {",
            "        Console.WriteLine(\"Hello\");",
            "    }",
            "    // [<endsnippet MySnippet>]",
            "}"
        };

        var result = CodeContentHelper.ExtractSnippet( lines, "MySnippet" );

        Assert.NotNull( result );
        Assert.Contains( "public void Method()", result );
        Assert.Contains( "Console.WriteLine", result );
        Assert.DoesNotContain( "[<snippet", result );
        Assert.DoesNotContain( "public class Outer", result );
    }

    [Fact]
    public void ExtractSnippet_RemovesCommonIndentation()
    {
        var lines = new[]
        {
            "public class Test",
            "{",
            "    // [<snippet MySnippet>]",
            "        var x = 1;",
            "        var y = 2;",
            "    // [<endsnippet MySnippet>]",
            "}"
        };

        var result = CodeContentHelper.ExtractSnippet( lines, "MySnippet" );

        Assert.NotNull( result );
        Assert.StartsWith( "var x = 1;", result, StringComparison.Ordinal );
    }

    [Fact]
    public void ExtractSnippet_ReturnsNullForMissingMarker()
    {
        var lines = new[]
        {
            "using System;",
            "public class Test { }"
        };

        var result = CodeContentHelper.ExtractSnippet( lines, "NonExistent" );

        Assert.Null( result );
    }

    [Fact]
    public void ExtractSnippet_HandlesNestedSnippetMarkers()
    {
        var lines = new[]
        {
            "// [<snippet Outer>]",
            "public class Test",
            "{",
            "    // [<snippet Inner>]",
            "    public void Method() { }",
            "    // [<endsnippet Inner>]",
            "}",
            "// [<endsnippet Outer>]"
        };

        var result = CodeContentHelper.ExtractSnippet( lines, "Outer" );

        Assert.NotNull( result );
        Assert.Contains( "public class Test", result );
        Assert.Contains( "public void Method()", result );
        Assert.DoesNotContain( "[<snippet Inner>]", result );
        Assert.DoesNotContain( "[<endsnippet Inner>]", result );
    }
}