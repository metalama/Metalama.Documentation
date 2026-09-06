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
using PostSharp.Engineering.BuildTools.Build.Publishing.Downloads;
using PostSharp.Engineering.BuildTools.ContinuousIntegration;
using PostSharp.Engineering.BuildTools.Docker;
using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.DocFx;
using System.IO;
using System.IO.Compression;
using MetalamaDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.MetalamaDependencies.V2027_0;

// The .NET 11 SDK, which global.json names as the main SDK of the product and which the build agent installs. The
// version is a literal instead of a member of the product family, because the .NET 11 SDK is still a preview and
// PostSharp.Engineering names only released feature bands. Keep it equal to the constant of the same name in the
// Metalama repository, and move both to MetalamaDependencies.Family.PreferredVersions.DotNetSdk once the .NET 11
// SDK is released.
const string dotNet11SdkVersion = "11.0.100-preview.7.26381.103";

// The .NET 10 SDK, which stays installed beside the .NET 11 one, because the build tool of this repository targets
// net10.0 and the .NET 11 SDK carries no .NET 10 runtime. The version comes from the product family, so that it
// matches the feature band that the Visual Studio version of the family installs.
var dotNet10SdkVersion = MetalamaDependencies.Family.PreferredVersions.DotNetSdk.V_10_0;

var docPackageFileName = $"Metalama.Doc.{MetalamaDependencies.Metalama.ProductFamily.Version}.zip";
var marketplacePackageFileName = $"Metalama.AI.Skills.*.zip";

var product = new Product( MetalamaDependencies.MetalamaDocumentation )
{
    // Note that we don't build Metalama.Samples ourselves. We expect it to be built from the repo itself.
    // HTML artifacts should be restored from artifacts.
    OverriddenBuildAgentRequirements = new ContainerRequirements( ContainerHostKind.Windows )
    {
        Components =
        [
            // Required for the rest.
            new DotNetComponent( dotNet11SdkVersion, DotNetComponentKind.Sdk ),
            new DotNetComponent( dotNet10SdkVersion, DotNetComponentKind.Sdk ),
        ]
    },
    GenerateNuGetConfig = true,
    DotNetSdkVersion = new DotNetSdkVersion( dotNet11SdkVersion ) { AllowPrerelease = true },
    
    Solutions =
    [
        new DotNetSolution( "code\\Metalama.Documentation.Prerequisites.sln" ) { CanFormatCode = true },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.TestBased.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Test },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.ProjectBased.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Build },
        new DocFxApiSolution( "docfx.json" ),
        new DocFxSiteSolution( "docfx.json", docPackageFileName ),
        new ClaudeMarketplaceSolution()
    ],
    PublicArtifacts = Pattern.Create( docPackageFileName, marketplacePackageFileName ),
    AdditionalDirectoriesToClean = [Path.Combine( "artifacts", "api" ), Path.Combine( "artifacts", "site" ), Path.Combine( "artifacts", "marketplace" )],
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
                        "https://postsharp-helpbrowser.azurewebsites.net/" ),
                    new GitRepoPublisher(
                        Pattern.Create( marketplacePackageFileName ),
                        "https://github.com/metalama/Metalama.AI.Skills",
                        $"Updated to {MetalamaDependencies.Metalama.ProductFamily.Version}." )
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
    ],
    AdditionalGitHubTokenRepositories = [new GitHubRepository( "Metalama.AI.Skills", "metalama" )]
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
        Path.Combine( args.Context.RepoDirectory, "source-dependencies", "Metalama.Samples", "src" );

    if ( File.Exists( htmlSourceZipFile ) )
    {
        args.Context.Console.WriteMessage( $"Restoring HTML files from '{htmlSourceZipFile}'." );
        Directory.CreateDirectory( htmlTargetDirectory );
        ZipFile.ExtractToDirectory( htmlSourceZipFile, htmlTargetDirectory, true );
    }
}