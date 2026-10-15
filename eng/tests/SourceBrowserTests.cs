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
        Assert.Equal( ["sample-log-1"], directory.Topics );

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
