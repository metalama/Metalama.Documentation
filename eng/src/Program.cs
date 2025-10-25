// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Amazon;
using BuildMetalamaDocumentation;
using BuildMetalamaDocumentation.Markdig.AspectTests;
using BuildMetalamaDocumentation.Markdig.CompareFile;
using BuildMetalamaDocumentation.Markdig.MultipleFiles;
using BuildMetalamaDocumentation.Markdig.ProjectButtons;
using BuildMetalamaDocumentation.Markdig.SingleFiles;
using BuildMetalamaDocumentation.Markdig.Vimeo;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Build.Publishing;
using PostSharp.Engineering.BuildTools.Docker;
using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.DocFx;
using System.IO;
using System.IO.Compression;
using MetalamaDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.MetalamaDependencies.V2026_0;

var docPackageFileName = $"Metalama.Doc.{MetalamaDependencies.Metalama.ProductFamily.Version}.zip";
const string dotNetSdkVersion = "10.0.100-rc.1.25451.107";

var product = new Product( MetalamaDependencies.MetalamaDocumentation )
{
    // Note that we don't build Metalama.Samples ourselves. We expect it to be built from the repo itself.
    // HTML artifacts should be restored from artifacts.
    OverriddenBuildAgentRequirements = new ContainerRequirements( ContainerHostKind.Windows )
    {
        Components =
        [
            // Required for eng.
            new DotNetComponent( "9.0.305", DotNetComponentKind.Sdk ),
            
            // Required for the rest.
            new DotNetComponent( dotNetSdkVersion, DotNetComponentKind.Sdk ),
        ]
    },
    GenerateNuGetConfig = true,
    DotNetSdkVersion = new DotNetSdkVersion( dotNetSdkVersion ),
    
    Solutions =
    [
        new DotNetSolution( "code\\Metalama.Documentation.Prerequisites.sln" ) { CanFormatCode = true },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.TestBased.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Test },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.ProjectBased.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Build },
        new DocFxApiSolution( "docfx.json" ),
        new DocFxSiteSolution( "docfx.json", docPackageFileName )
    ],
    PublicArtifacts = Pattern.Create( docPackageFileName ),
    AdditionalDirectoriesToClean = [Path.Combine( "artifacts", "api" ), Path.Combine( "artifacts", "site" )],
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )
        .WithValue(
            BuildConfiguration.Public,
            c => c with
            {
                ExportsToTeamCityDeployWithoutDependencies = true,
                PublicPublishers =
                [
                    new DocumentationPublisher(
                        [new( docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net", docPackageFileName )],
                        "https://postsharp-helpbrowser.azurewebsites.net/" )
                ]
            } ),
    Extensions =
    [
        // Run `b generate-scripts` after changing these parameters.
        new UpdateSearchProductExtension(
            "https://typesense.postsharp.net",
            "metalamadoc",
            "https://doc-production.metalama.net/sitemap.xml",
            () => new MetalamaDocCrawler(),
            ["Metalama"] )
    ]
};

product.PrepareCompleted += OnPrepareCompleted;

var app = new EngineeringApp( product );

app.AddDocFxCommands(
    new DocFxOptions
    {
        ConfigureMarkdig = markdig =>
        {
            markdig.Extensions.AddIfNotAlready<AspectTestInlineExtension>();
            markdig.Extensions.AddIfNotAlready<SingleFileInlineExtension>();
            markdig.Extensions.AddIfNotAlready<CompareFileInlineExtension>();
            markdig.Extensions.AddIfNotAlready<ProjectButtonsInlineExtension>();
            markdig.Extensions.AddIfNotAlready<MultipleFilesInlineExtension>();
            markdig.Extensions.AddIfNotAlready<VimeoInlineExtension>();
        }
    } );

return app.Run( args );

static void OnPrepareCompleted( PrepareCompletedEventArgs args )
{
    // Extract HTML artefact dependencies to the source dependency directory.
    var htmlSourceZipFile =
        Path.Combine( args.Context.RepoDirectory, "dependencies", "Metalama.Samples", "html-examples.zip" );

    var htmlTargetDirectory =
        Path.Combine( args.Context.RepoDirectory, "source-dependencies", "Metalama.Samples", "examples" );

    if ( File.Exists( htmlSourceZipFile ) )
    {
        args.Context.Console.WriteMessage( $"Restoring HTML files from '{htmlSourceZipFile}'." );
        Directory.CreateDirectory( htmlTargetDirectory );
        ZipFile.ExtractToDirectory( htmlSourceZipFile, htmlTargetDirectory, true );
    }
}