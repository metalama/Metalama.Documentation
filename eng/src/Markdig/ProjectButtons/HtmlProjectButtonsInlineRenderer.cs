// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System;
using System.IO;
using System.Linq;

namespace BuildMetalamaDocumentation.Markdig.ProjectButtons;

public class HtmlProjectButtonsInlineRenderer : HtmlObjectRenderer<ProjectButtonsInline>
{
    protected override void Write( HtmlRenderer renderer, ProjectButtonsInline obj )
    {
        var directory = obj.Directory;

        var id = Path.GetFileNameWithoutExtension( directory )
            .ToLowerInvariant()
            .Replace( '.', '_' );

        var tabGroup = new DirectoryTabGroup( id, directory );

        foreach ( var file in Directory.GetFiles( directory, "*.cs" ) )
        {
            var lines = File.ReadAllLines( file );

            var kind = lines.Any( l => l.StartsWith( "using Metalama.Framework", StringComparison.Ordinal ) )
                ? SandboxFileKind.AspectCode
                : SandboxFileKind.None;

            if ( kind == SandboxFileKind.None )
            {
                // We need to try harder to find the good category.

                var outHtmlPath = PathHelper.GetObjPaths( obj.Directory, file, ".t.cs.html" )
                    .FirstOrDefault( File.Exists );

                if ( outHtmlPath == null || !File.ReadAllText( outHtmlPath ).Contains( "cr-GeneratedCode", StringComparison.Ordinal ) )
                {
                    kind = SandboxFileKind.ExtraCode;
                }
                else
                {
                    kind = SandboxFileKind.TargetCode;
                }
            }

            tabGroup.Tabs.Add(
                new CodeTab(
                    Path.GetFileNameWithoutExtension( file ).ToLowerInvariant(),
                    file,
                    kind ) );
        }

        renderer.WriteLine(
            @"
<div class=""project-buttons"">");
        
        var gitUrl = tabGroup.GetGitUrl();

        renderer.WriteLine( $@"    <a href=""{gitUrl}"" class=""github"">See on GitHub</a>" );
        
        if ( tabGroup.IsSandboxEnabled )
        {
            var sandboxPayload = tabGroup.GetSandboxPayload( obj );

            if ( sandboxPayload == null )
            {
                throw new InvalidOperationException( $"Cannot load '{directory}' into the sandbox." );
            }

            renderer.WriteLine(
                $@"    <a role=""button"" class=""sandbox"" onclick=""openSandbox('{sandboxPayload}')"" target=""github"">Open in sandbox</a>" );
        }

        renderer.WriteLine( "</div>" );
    }
}