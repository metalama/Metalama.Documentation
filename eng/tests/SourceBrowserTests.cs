// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.SourceBrowser;

namespace BuildMetalamaDocumentation.UnitTests;

public sealed class SourceBrowserTests : IDisposable
{
    private readonly string _repoDirectory = Path.Combine( Path.GetTempPath(), "SourceBrowserTests", Guid.NewGuid().ToString( "N" ) );
    private readonly List<string> _warnings = [];

    public SourceBrowserTests()
    {
        // A sample project whose README declares its directory.
        this.WriteFile(
            "samples/src/log/log-1/README.md",
            """
            ---
            uid: sample-log-1
            browse-source: .
            ---
            # Log
            [!metalama-compare Calculator.cs]
            """ );

        this.WriteFile( "samples/src/log/log-1/Sample.Log1.csproj", "<Project/>" );
        this.WriteFile( "samples/src/log/log-1/LogAttribute.cs", "using Metalama.Framework.Aspects;\nclass LogAttribute {}" );
        this.WriteFile( "samples/src/log/log-1/Calculator.cs", "class Calculator {}" );
        this.WriteFile( "samples/src/log/log-1/Program.cs", "class Program {}" );
        this.WriteFile( "samples/src/log/log-1/obj/html/net10.0/Calculator.cs.html", "<pre><code>class Calculator {}</code></pre>" );
        this.WriteFile( "samples/src/log/log-1/obj/html/net10.0/Calculator.t.cs.html", "<pre><code>class Calculator { int x; }</code></pre>" );
        this.WriteFile( "samples/src/log/log-1/obj/html/net10.0/Program.cs.html", "<pre><code>class Program {}</code></pre>" );
        this.WriteFile( "samples/src/log/log-1/obj/html/net10.0/Program.t.cs.html", "<pre><code>class  Program  {}</code></pre>" );
        this.WriteFile( "samples/src/log/log-1/bin/Debug/Sample.Log1.dll", "" );

        // An article that includes a file of the sample, and an article that declares a missing directory.
        this.WriteFile(
            "content/logging.md",
            """
            ---
            uid: logging
            ---
            [!metalama-file ../samples/src/log/log-1/Calculator.cs marker="Main" name="The calculator"]
            """ );

        this.WriteFile(
            "samples/src/broken/README.md",
            """
            ---
            uid: broken
            browse-source: missing
            ---
            """ );

        this.WriteFile(
            "docfx.json",
            """
            {
              // Comments are allowed.
              "build": {
                "content": [
                  { "files": [ "**.md" ], "src": "content" },
                  { "files": [ "src/**.md" ], "src": "samples" }
                ]
              }
            }
            """ );
    }

    private void WriteFile( string relativePath, string content )
    {
        var path = Path.Combine( this._repoDirectory, relativePath );
        Directory.CreateDirectory( Path.GetDirectoryName( path )! );
        File.WriteAllText( path, content );
    }

    private SourceBrowserManifest Generate()
        => new SourceBrowserGenerator(
            this._repoDirectory,
            [new SourceBrowserRoot( Path.Combine( "samples", "src" ), "samples" )],
            ["content"],
            this._warnings.Add ).Generate();

    [Fact]
    public void Generate_PublishesDeclaredDirectory()
    {
        var manifest = this.Generate();

        var directory = Assert.Single( manifest.Directories );
        Assert.Equal( "source/samples/log/log-1", directory.Path );
        Assert.Equal( "Sample.Log1", directory.Title );

        // A path alone declares a single node titled "Source code" under the article.
        var mount = Assert.Single( manifest.Mounts );
        Assert.Equal( new SourceBrowserMount( "sample-log-1", "Source code", "source/samples/log/log-1", [] ), mount with { Children = [] } );
        Assert.Empty( mount.Children );

        // Build outputs and Markdown files are skipped, and the project file is first.
        Assert.Equal(
            ["Sample.Log1.csproj", "Calculator.cs", "LogAttribute.cs", "Program.cs"],
            directory.Files.Select( f => f.Path ) );

        var outputDirectory = Path.Combine( SourceBrowserGenerator.GetOutputDirectory( this._repoDirectory ), "samples", "log", "log-1" );
        Assert.True( File.Exists( Path.Combine( outputDirectory, "Calculator.cs" ) ) );
        Assert.True( File.Exists( Path.Combine( SourceBrowserGenerator.GetOutputDirectory( this._repoDirectory ), "manifest.json" ) ) );
    }

    [Fact]
    public void Generate_ClassifiesFilesAndCopiesHtml()
    {
        var files = Assert.Single( this.Generate().Directories ).Files.ToDictionary( f => f.Path );

        Assert.Equal( "aspect", files["LogAttribute.cs"].Role );
        Assert.Null( files["LogAttribute.cs"].SourceHtml );

        Assert.Equal( "target", files["Calculator.cs"].Role );
        Assert.Equal( "Calculator.cs.source.html", files["Calculator.cs"].SourceHtml );
        Assert.Equal( "Calculator.cs.transformed.html", files["Calculator.cs"].TransformedHtml );

        // The transformed code of Program.cs only differs by white space, so it is omitted.
        Assert.Equal( "other", files["Program.cs"].Role );
        Assert.Equal( "Program.cs.source.html", files["Program.cs"].SourceHtml );
        Assert.Null( files["Program.cs"].TransformedHtml );
    }

    [Fact]
    public void Generate_FindsReferencingArticles()
    {
        var files = Assert.Single( this.Generate().Directories ).Files.ToDictionary( f => f.Path );

        Assert.Equal( ["sample-log-1", "logging"], files["Calculator.cs"].Topics );
        Assert.Empty( files["LogAttribute.cs"].Topics );
    }

    [Fact]
    public void Generate_WarnsAboutMissingDirectory()
    {
        this.Generate();

        Assert.Contains( this._warnings, w => w.Contains( "missing", StringComparison.Ordinal ) );
    }

    [Fact]
    public void Generate_FullForm_DeclaresTitledAndNestedNodes()
    {
        this.WriteFile(
            "samples/src/log/log-2/README.md",
            """
            ---
            uid: sample-log-2
            browse-source:
              - path: .
                children:
                  - title: Tests
                    path: ../../tests/Sample.Log2.Tests
              - title: Shared library
                path: ../shared
            ---
            """ );

        this.WriteFile( "samples/src/log/log-2/Sample.Log2.csproj", "<Project/>" );
        this.WriteFile( "samples/src/tests/Sample.Log2.Tests/Sample.Log2.Tests.csproj", "<Project/>" );
        this.WriteFile( "samples/src/tests/Sample.Log2.Tests/CalculatorTests.cs", "class CalculatorTests {}" );
        this.WriteFile( "samples/src/log/shared/Shared.cs", "class Shared {}" );

        var manifest = this.Generate();

        var mounts = manifest.Mounts.Where( m => m.Topic == "sample-log-2" ).ToList();

        // With several top-level nodes, a node without title is named after its project.
        Assert.Equal( ["Sample.Log2", "Shared library"], mounts.Select( m => m.Title ) );
        Assert.Equal( ["source/samples/log/log-2", "source/samples/log/shared"], mounts.Select( m => m.Directory ) );

        // A child node has no topic: it is shown under its parent node.
        var tests = Assert.Single( mounts[0].Children );
        Assert.Equal( new SourceBrowserMount( null, "Tests", "source/samples/tests/Sample.Log2.Tests", [] ), tests with { Children = [] } );

        Assert.Contains( manifest.Directories, d => d.Path == "source/samples/tests/Sample.Log2.Tests" && d.Files.Count == 2 );
    }

    [Fact]
    public void Generate_ChildInsideItsParent_IsPublishedSeparately()
    {
        this.WriteFile(
            "samples/src/log/log-3/README.md",
            """
            ---
            uid: sample-log-3
            browse-source:
              - path: .
                children:
                  - title: Tests
                    path: Tests
            ---
            """ );

        this.WriteFile( "samples/src/log/log-3/Calculator.cs", "class Calculator {}" );
        this.WriteFile( "samples/src/log/log-3/Tests/CalculatorTests.cs", "class CalculatorTests {}" );

        var manifest = this.Generate();

        // The files of the child directory are not listed in the parent directory.
        var parent = manifest.Directories.Single( d => d.Path == "source/samples/log/log-3" );
        var child = manifest.Directories.Single( d => d.Path == "source/samples/log/log-3/Tests" );

        Assert.Equal( ["Calculator.cs"], parent.Files.Select( f => f.Path ) );
        Assert.Equal( ["CalculatorTests.cs"], child.Files.Select( f => f.Path ) );

        // A link to a file of the child directory goes to the child directory.
        var testFile = Path.Combine( this._repoDirectory, "samples", "src", "log", "log-3", "Tests", "CalculatorTests.cs" );

        Assert.Equal(
            "source/samples/log/log-3/Tests/CalculatorTests.cs",
            SourceBrowserLinks.GetHref( this._repoDirectory, "content/logging.md", testFile ) );
    }

    [Fact]
    public void Generate_NodeWithoutPath_IsReported()
    {
        this.WriteFile(
            "samples/src/log/log-4/README.md",
            """
            ---
            uid: sample-log-4
            browse-source:
              - title: Nothing
            ---
            """ );

        var manifest = this.Generate();

        Assert.DoesNotContain( manifest.Mounts, m => m.Topic == "sample-log-4" );
        Assert.Contains( this._warnings, w => w.Contains( "without path", StringComparison.Ordinal ) );
    }

    [Fact]
    public void GetHref_IsRelativeToTheArticlePage()
    {
        this.Generate();

        var calculator = Path.Combine( this._repoDirectory, "samples", "src", "log", "log-1", "Calculator.cs" );
        var sampleDirectory = Path.Combine( this._repoDirectory, "samples", "src", "log", "log-1" );

        // The article content/logging.md is published as logging.html.
        Assert.Equal( "source/samples/log/log-1/Calculator.cs", SourceBrowserLinks.GetHref( this._repoDirectory, "content/logging.md", calculator ) );

        // The article samples/src/log/log-1/README.md is published as src/log/log-1/README.html.
        Assert.Equal(
            "../../../source/samples/log/log-1",
            SourceBrowserLinks.GetHref( this._repoDirectory, "samples/src/log/log-1/README.md", sampleDirectory ) );

        // A file that is not published gives no link.
        Assert.Null( SourceBrowserLinks.GetHref( this._repoDirectory, "content/logging.md", Path.Combine( this._repoDirectory, "docfx.json" ) ) );
    }

    public void Dispose()
    {
        try
        {
            Directory.Delete( this._repoDirectory, true );
        }
        catch ( IOException ) { }
    }
}
