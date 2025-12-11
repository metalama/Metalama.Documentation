// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildMetalamaDocumentation;

/// <summary>
/// Solution that generates the Claude SKILL artifact from documentation.
/// </summary>
internal class ClaudeSkillSolution : Solution
{
    public ClaudeSkillSolution() : base( "Claude SKILL" )
    {
        this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Pack;
    }

    public override bool Build( BuildContext context, BuildSettings settings )
    {
        var repoDir = context.RepoDirectory;
        var skillOutputDir = Path.Combine( repoDir, "artifacts", "skill" );

        context.Console.WriteHeading( "Building Claude SKILL artifact" );

        try
        {
            // Clean and create output directory
            if ( Directory.Exists( skillOutputDir ) )
            {
                Directory.Delete( skillOutputDir, true );
            }

            Directory.CreateDirectory( skillOutputDir );

            // 1. Copy SKILL.md from claude/SKILL.md and replace version placeholder
            var skillSourcePath = Path.Combine( repoDir, "claude", "SKILL.md" );
            var skillDestPath = Path.Combine( skillOutputDir, "SKILL.md" );

            if ( File.Exists( skillSourcePath ) )
            {
                var skillContent = File.ReadAllText( skillSourcePath );
                var version = context.Product.ProductFamily.Version;
                skillContent = skillContent.Replace( "<version>", version, StringComparison.Ordinal );
                File.WriteAllText( skillDestPath, skillContent );
                context.Console.WriteMessage( $"Copied SKILL.md (version: {version})" );
            }
            else
            {
                context.Console.WriteWarning( $"SKILL.md not found at {skillSourcePath}" );
            }

            // 2. Copy content/**/*.md (Markdown documentation)
            var contentSourceDir = Path.Combine( repoDir, "content" );
            var contentDestDir = Path.Combine( skillOutputDir, "content" );

            CopyDirectory( contentSourceDir, contentDestDir, "*.md", context );
            CopyDirectory( contentSourceDir, contentDestDir, "*.yml", context );

            context.Console.WriteMessage( "Copied content/ directory" );

            // 3. Copy code/**/*.cs (sample code, verbatim)
            var codeSourceDir = Path.Combine( repoDir, "code" );
            var codeDestDir = Path.Combine( skillOutputDir, "code" );

            CopyDirectory( codeSourceDir, codeDestDir, "*.cs", context );

            context.Console.WriteMessage( "Copied code/ directory" );

            // 4. Copy artifacts/api/*.yml and .manifest (API documentation)
            var apiSourceDir = Path.Combine( repoDir, "artifacts", "api" );
            var apiDestDir = Path.Combine( skillOutputDir, "api" );

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

            // 5. Generate index.yml
            var indexGenerator = new SkillIndexGenerator( repoDir, context.Console );
            var indexContent = indexGenerator.GenerateIndex();
            var indexPath = Path.Combine( skillOutputDir, "index.yml" );

            File.WriteAllText( indexPath, indexContent );
            context.Console.WriteMessage( "Generated index.yml" );

            context.Console.WriteSuccess( $"Claude SKILL artifact created at {skillOutputDir}" );

            return true;
        }
        catch ( Exception ex )
        {
            context.Console.WriteError( $"Failed to build Claude SKILL: {ex.Message}" );

            return false;
        }
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
        var skillOutputDir = Path.Combine( repoDir, "artifacts", "skill" );
        var publishDir = Path.Combine( repoDir, "artifacts", "publish", "private" );
        var zipFileName = $"Metalama.Skill.{buildArguments.PackageVersion}.zip";
        var zipPath = Path.Combine( publishDir, zipFileName );

        try
        {
            // Ensure publish directory exists
            Directory.CreateDirectory( publishDir );

            // Delete existing zip if present
            if ( File.Exists( zipPath ) )
            {
                File.Delete( zipPath );
            }

            // Create zip from skill directory
            ZipFile.CreateFromDirectory( skillOutputDir, zipPath, CompressionLevel.Optimal, false );

            context.Console.WriteSuccess( $"Created {zipFileName} in {publishDir}" );

            return true;
        }
        catch ( Exception ex )
        {
            context.Console.WriteError( $"Failed to pack Claude SKILL: {ex.Message}" );

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
