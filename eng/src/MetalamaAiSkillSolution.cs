// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.DocFx.AiSkills;
using System.IO;

namespace BuildMetalamaDocumentation;

/// <summary>
/// An <see cref="AiSkillSolution"/> that also copies the MCP server configuration of the plugin, <c>.mcp.json</c>,
/// from the skill source directory to the root of the plugin.
/// </summary>
/// <remarks>
/// Claude Code reads the <c>.mcp.json</c> file at the root of a plugin and starts the MCP servers that the file declares.
/// The file registers <c>metalama-mcp</c>, the tool of the <c>Metalama.Mcp</c> package.
/// <see cref="AiSkillSolution.Pack(BuildContext, BuildSettings)"/> calls <see cref="Build(BuildContext, BuildSettings)"/>
/// before it creates the zip file, so the zip file that the publisher pushes contains the configuration.
/// </remarks>
internal sealed class MetalamaAiSkillSolution : AiSkillSolution
{
    private const string _mcpConfigurationFileName = ".mcp.json";

    private readonly AiSkillOptions _options;

    public MetalamaAiSkillSolution( AiSkillOptions options ) : base( options )
    {
        this._options = options;
    }

    public override bool Build( BuildContext context, BuildSettings settings )
    {
        if ( !base.Build( context, settings ) )
        {
            return false;
        }

        var sourcePath = Path.Combine( context.RepoDirectory, this._options.SkillSourceDirectory, _mcpConfigurationFileName );

        // AiSkillSolution builds the marketplace in artifacts/marketplace, with one directory per plugin.
        var pluginDirectory = Path.Combine( context.RepoDirectory, "artifacts", "marketplace", "plugins", this._options.PluginName );

        if ( !File.Exists( sourcePath ) )
        {
            context.Console.WriteError( $"The MCP server configuration was not found at {sourcePath}." );

            return false;
        }

        File.Copy( sourcePath, Path.Combine( pluginDirectory, _mcpConfigurationFileName ), true );
        context.Console.WriteMessage( $"Copied {_mcpConfigurationFileName} to the root of the plugin." );

        return true;
    }
}
