// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildMetalamaDocumentation;

/// <summary>
/// Solution that generates the Claude marketplace artifact from documentation.
/// </summary>
internal class ClaudeMarketplaceSolution : Solution
{
    public ClaudeMarketplaceSolution() : base( "Claude Marketplace" )
    {
        this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Pack;
    }

    public override bool Build( BuildContext context, BuildSettings settings )
    {
        var repoDir = context.RepoDirectory;
        var marketplaceOutputDir = Path.Combine( repoDir, "artifacts", "marketplace" );
        var version = context.Product.ProductFamily.Version;

        context.Console.WriteHeading( "Building Claude Marketplace artifact" );

        try
        {
            // Clean and create output directory
            if ( Directory.Exists( marketplaceOutputDir ) )
            {
                Directory.Delete( marketplaceOutputDir, true );
            }

            Directory.CreateDirectory( marketplaceOutputDir );

            // Define plugin and skill paths
            var pluginDir = Path.Combine( marketplaceOutputDir, "plugins", "metalama" );
            var pluginConfigDir = Path.Combine( pluginDir, ".claude-plugin" );
            var skillDir = Path.Combine( pluginDir, "skills", "metalama" );

            Directory.CreateDirectory( pluginConfigDir );
            Directory.CreateDirectory( skillDir );

            // 1. Generate marketplace.json at the root
            GenerateMarketplaceJson( marketplaceOutputDir, version );
            context.Console.WriteMessage( "Generated marketplace.json" );

            // 2. Generate .claude-plugin/plugin.json
            GeneratePluginJson( pluginConfigDir, version );
            context.Console.WriteMessage( "Generated plugin.json" );

            // 3. Copy README.md from claude/README.md to marketplace root
            var readmeSourcePath = Path.Combine( repoDir, "claude", "README.md" );
            var readmeDestPath = Path.Combine( marketplaceOutputDir, "README.md" );

            if ( File.Exists( readmeSourcePath ) )
            {
                File.Copy( readmeSourcePath, readmeDestPath, true );
                context.Console.WriteMessage( "Copied README.md" );
            }

            // 4. Copy SKILL.md from claude/SKILL.md and replace version placeholder
            var skillSourcePath = Path.Combine( repoDir, "claude", "SKILL.md" );
            var skillDestPath = Path.Combine( skillDir, "SKILL.md" );

            if ( File.Exists( skillSourcePath ) )
            {
                var skillContent = File.ReadAllText( skillSourcePath );
                skillContent = skillContent.Replace( "<version>", version, StringComparison.Ordinal );
                File.WriteAllText( skillDestPath, skillContent );
                context.Console.WriteMessage( $"Copied SKILL.md (version: {version})" );
            }
            else
            {
                context.Console.WriteWarning( $"SKILL.md not found at {skillSourcePath}" );
            }

            // 5. Copy content/**/*.md (Markdown documentation)
            var contentSourceDir = Path.Combine( repoDir, "content" );
            var contentDestDir = Path.Combine( skillDir, "content" );

            CopyDirectory( contentSourceDir, contentDestDir, "*.md", context );
            CopyDirectory( contentSourceDir, contentDestDir, "*.yml", context );

            context.Console.WriteMessage( "Copied content/ directory" );

            // 6. Copy code/**/*.cs (sample code, verbatim)
            var codeSourceDir = Path.Combine( repoDir, "code" );
            var codeDestDir = Path.Combine( skillDir, "code" );

            CopyDirectory( codeSourceDir, codeDestDir, "*.cs", context );

            context.Console.WriteMessage( "Copied code/ directory" );

            // 7. Copy artifacts/api/*.yml and .manifest (API documentation)
            var apiSourceDir = Path.Combine( repoDir, "artifacts", "api" );
            var apiDestDir = Path.Combine( skillDir, "api" );

            if ( Directory.Exists( apiSourceDir ) )
            {
                CopyDirectory( apiSourceDir, apiDestDir, "*.yml", context );

                // Copy .manifest file
                var manifestSource = Path.Combine( apiSourceDir, ".manifest" );
                var manifestDest = Path.Combine( apiDestDir, ".manifest" );

                if ( File.Exists( manifestSource ) )
                {
                    Directory.CreateDirectory( apiDestDir );
                    File.Copy( manifestSource, manifestDest );
                }

                context.Console.WriteMessage( "Copied api/ directory" );
            }
            else
            {
                context.Console.WriteWarning( $"API directory not found at {apiSourceDir}. Run DocFx API generation first." );
            }

            // 8. Generate index.yml
            var indexGenerator = new SkillIndexGenerator( repoDir, context.Console );
            var indexContent = indexGenerator.GenerateIndex();
            var indexPath = Path.Combine( skillDir, "index.yml" );

            File.WriteAllText( indexPath, indexContent );
            context.Console.WriteMessage( "Generated index.yml" );

            context.Console.WriteSuccess( $"Claude Marketplace artifact created at {marketplaceOutputDir}" );

            return true;
        }
        catch ( Exception ex )
        {
            context.Console.WriteError( $"Failed to build Claude Marketplace: {ex.Message}" );

            return false;
        }
    }

    private static void GenerateMarketplaceJson( string marketplaceDir, string version )
    {
        var marketplace = new
        {
            name = "metalama",
            owner = new
            {
                name = "PostSharp Technologies",
                email = "hello@postsharp.net"
            },
            description = "Metalama documentation and tools for aspect-oriented programming in C#",
            plugins = new[]
            {
                new
                {
                    name = "metalama",
                    source = "./plugins/metalama",
                    description = "Complete Metalama documentation for aspect-oriented programming in C#. Use when writing aspects, templates, fabrics, or meta-programming code with Metalama.",
                    version = version
                }
            }
        };

        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize( marketplace, options );
        File.WriteAllText( Path.Combine( marketplaceDir, "marketplace.json" ), json );
    }

    private static void GeneratePluginJson( string pluginConfigDir, string version )
    {
        var plugin = new
        {
            name = "metalama",
            version = version,
            description = "Complete Metalama documentation for aspect-oriented programming in C#. Use when writing aspects, templates, fabrics, or meta-programming code with Metalama."
        };

        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize( plugin, options );
        File.WriteAllText( Path.Combine( pluginConfigDir, "plugin.json" ), json );
    }

    private static void CopyDirectory( string sourceDir, string destDir, string pattern, BuildContext context )
    {
        if ( !Directory.Exists( sourceDir ) )
        {
            return;
        }

        foreach ( var file in Directory.GetFiles( sourceDir, pattern, SearchOption.AllDirectories ) )
        {
            var relativePath = Path.GetRelativePath( sourceDir, file );
            var destPath = Path.Combine( destDir, relativePath );
            var destDirectory = Path.GetDirectoryName( destPath )!;

            Directory.CreateDirectory( destDirectory );
            File.Copy( file, destPath, true );
        }
    }

    public override bool Pack( BuildContext context, BuildSettings settings )
    {
        // Build first to ensure artifacts exist
        if ( !this.Build( context, settings ) )
        {
            return false;
        }

        // Get full package version
        if ( !BuildArguments.TryReadFromAutoUpdatedVersionsFile( context, settings.BuildConfiguration, out var buildArguments ) )
        {
            context.Console.WriteError( "Failed to read package version from AutoUpdatedVersions.props" );

            return false;
        }

        var repoDir = context.RepoDirectory;
        var marketplaceOutputDir = Path.Combine( repoDir, "artifacts", "marketplace" );
        var publishDir = Path.Combine( repoDir, "artifacts", "publish", "private" );
        var zipFileName = $"Metalama.AI.Skills.{buildArguments.PackageVersion}.zip";
        var zipPath = Path.Combine( publishDir, zipFileName );

        try
        {
            // Update JSON files with the full package version
            var pluginConfigDir = Path.Combine( marketplaceOutputDir, "plugins", "metalama", ".claude-plugin" );
            GenerateMarketplaceJson( marketplaceOutputDir, buildArguments.PackageVersion );
            GeneratePluginJson( pluginConfigDir, buildArguments.PackageVersion );
            context.Console.WriteMessage( $"Updated JSON files with package version: {buildArguments.PackageVersion}" );

            // Ensure publish directory exists
            Directory.CreateDirectory( publishDir );

            // Delete existing zip if present
            if ( File.Exists( zipPath ) )
            {
                File.Delete( zipPath );
            }

            // Create zip from marketplace directory
            ZipFile.CreateFromDirectory( marketplaceOutputDir, zipPath, CompressionLevel.Optimal, false );

            context.Console.WriteSuccess( $"Created {zipFileName} in {publishDir}" );

            return true;
        }
        catch ( Exception ex )
        {
            context.Console.WriteError( $"Failed to pack Claude Marketplace: {ex.Message}" );

            return false;
        }
    }

    public override bool Restore( BuildContext context, BuildSettings options )
    {
        // No restore needed
        return true;
    }

    public override bool Test( BuildContext context, BuildSettings settings )
    {
        // No tests for this solution
        return true;
    }
}
