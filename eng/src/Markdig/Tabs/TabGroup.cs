// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Sandbox;
using Markdig.Renderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BuildMetalamaDocumentation.Markdig.Tabs;

internal abstract class TabGroup
{
    private string TabGroupId { get; }
    
#pragma warning disable CA1805 // Do not initialize unnecessarily
    // The sandbox needs to be enabled in HelpBrowser in MetalamaDoc.xslt.
    public bool IsSandboxEnabled { get; } = false;
#pragma warning restore CA1805

    public List<BaseTab> Tabs { get; } = new();

    public abstract string GetGitUrl();

    protected TabGroup( string tabGroupId )
    {
        if ( tabGroupId.Contains( ' ', StringComparison.Ordinal ) || tabGroupId.Contains( '.', StringComparison.Ordinal ) )
        {
            throw new ArgumentOutOfRangeException(
                nameof(tabGroupId),
                $"The id '{tabGroupId}' contains an invalid character." );
        }

        this.TabGroupId = tabGroupId;
    }

    public void Render( HtmlRenderer renderer, TabGroupBaseInline obj )
    {
        // Select tabs to render.
        var tabs = this.GetEnabledTabs( obj );

        if ( tabs.Count == 0 )
        {
            throw new InvalidOperationException( $"The tab group '{obj}' has no tab." );
        }

        // Define the wrapping div.
        var divId = $"code-{this.TabGroupId}";

        renderer.WriteLine( $"<div id=\"{divId}\" class=\"anchor\">" );

        if ( obj.AddLinks )
        {
            // Start the links.
            renderer.WriteLine( $@"<div class=""sample-links {(tabs.Count == 1 ? "" : "tabbed")}"">" );

            if ( this.IsSandboxEnabled )
            {
                // Create the sandbox link.
                var sandboxPayload = this.GetSandboxPayload( tabs );

                if ( sandboxPayload != null )
                {
                    renderer.WriteLine(
                        $@"  <a class=""try"" onclick=""openSandbox('{sandboxPayload}');"" role=""button"">Open in sandbox</a>" );
                    renderer.WriteLine( "<span class='separator'>|</span>" );
                }
            }

            // Git.
            var gitUrl = this.GetGitUrl();

            renderer.WriteLine(
                @$"
    <a class=""github"" href=""{gitUrl}"" target=""github"">See on GitHub</a>" );

            // Full screen. Skip the button when all tabs are small enough to fit without it.
            if ( tabs.Any( RequiresFullScreen ) )
            {
                renderer.WriteLine( "<span class='separator'>|</span>" );

                renderer.WriteLine(
                    @$"
    <a class=""fullscreen"" onclick=""toggleFullScreen('{divId}');"" role=""button"" target=""github"">Full screen</a>" );
            }

            // Close.
            renderer.WriteLine( "</div>" );
        }

        // Write the tabs.
        if ( tabs.Count == 1 )
        {
            // If there is a single file, we do not create a tab group.
            renderer.WriteLine( tabs[0].GetTabContent() );
        }
        else
        {
            renderer.WriteLine( @"<div class=""tabGroup""><ul>" );

            foreach ( var tab in tabs )
            {
                tab.AppendTabHeader( renderer, this.TabGroupId );
            }

            renderer.WriteLine( "</ul>" );

            foreach ( var tab in tabs )
            {
                tab.AppendTabBody( renderer, this.TabGroupId );
            }

            renderer.WriteLine( "</div>" );
        }

        // Emit JSON-LD block with plain code for AI discoverability.
        this.RenderJsonLd( renderer, tabs );

        // Close the wrapping div.
        renderer.WriteLine( "</div>" );
    }

    private void RenderJsonLd( HtmlRenderer renderer, List<BaseTab> tabs )
    {
        var files = new List<object>();

        foreach ( var tab in tabs )
        {
            var plainCode = GetPlainCode( tab );

            if ( plainCode != null )
            {
                files.Add( new { name = tab.TabId, description = GetTabDescription( tab ), content = plainCode } );
            }
        }

        if ( files.Count == 0 )
        {
            return;
        }

        var jsonLd = new
        {
            @context = "https://schema.org",
            @type = "SoftwareSourceCode",
            name = this.TabGroupId,
            programmingLanguage = "C#",
            codeSampleType = "code snippet",
            description = "Metalama code example. Files ending with .t.cs show the transformed code after Metalama has executed aspects. Compare the original source with the .t.cs file to see what code Metalama generated.",
            files
        };

        var json = JsonSerializer.Serialize( jsonLd, new JsonSerializerOptions { WriteIndented = false } );

        renderer.WriteLine( $"<script type=\"application/ld+json\">{json}</script>" );
    }

    private static string GetTabDescription( BaseTab tab )
    {
        return tab switch
        {
            TransformedSingleFileCodeTab => "Transformed code after Metalama has executed aspects",
            CodeTab codeTab when codeTab.FullPath.EndsWith( ".t.cs", StringComparison.OrdinalIgnoreCase ) => "Transformed code after Metalama has executed aspects",
            CodeTab codeTab when codeTab.FullPath.Contains( ".Aspect", StringComparison.OrdinalIgnoreCase ) => "Aspect code (compile-time)",
            CodeTab codeTab when codeTab.FullPath.Contains( ".Fabric", StringComparison.OrdinalIgnoreCase ) => "Fabric code (compile-time)",
            CompareTab => "Source code before transformation",
            ProgramOutputTab => "Program output when executed",
            CodeTab => "Source code",
            _ => "Code file"
        };
    }

    private static bool RequiresFullScreen( BaseTab tab )
    {
        var plainCode = GetPlainCode( tab );

        if ( plainCode == null )
        {
            // When the content cannot be measured, keep the button.
            return true;
        }

        var lines = plainCode.Split( '\n' );

        return lines.Length >= 10 || lines.Any( line => line.TrimEnd( '\r' ).Length >= 120 );
    }

    private static string? GetPlainCode( BaseTab tab )
    {
        string? filePath = tab switch
        {
            CodeTab codeTab when File.Exists( codeTab.FullPath ) => codeTab.FullPath,
            CompareTab compareTab when File.Exists( compareTab.FullPath ) => compareTab.FullPath,
            ProgramOutputTab outputTab when File.Exists( outputTab.FullPath ) => outputTab.FullPath,
            _ => null
        };

        if ( filePath == null )
        {
            return null;
        }

        // For program output, return as-is.
        if ( tab is ProgramOutputTab )
        {
            return File.ReadAllText( filePath );
        }

        // For code files, skip leading comments (e.g., copyright headers).
        var lines = File.ReadAllLines( filePath );

        return CodeContentHelper.ProcessCodeContent( lines );
    }

    private List<BaseTab> GetEnabledTabs( TabGroupBaseInline obj )
    {
        bool IsTabEnabled( BaseTab tab ) => (obj.Tabs.Length == 0 || obj.Tabs.Contains( tab.TabId )) && !tab.IsEmpty();

        var tabs = this.Tabs
            .Where( IsTabEnabled )
            .ToList();

        return tabs;
    }

    public string? GetSandboxPayload( TabGroupBaseInline obj ) => this.GetSandboxPayload( this.GetEnabledTabs( obj ) );

    private string? GetSandboxPayload( List<BaseTab> tabs )
    {
        var sandboxFiles = new List<SandboxFile>();
        var canOpenInSandbox = true;

        foreach ( var tab in tabs )
        {
            if ( tab is CodeTab codeTab )
            {
                if ( codeTab.SandboxFileKind == SandboxFileKind.Incompatible )
                {
                    canOpenInSandbox = false;

                    break;
                }

                if ( codeTab.SandboxFileKind != SandboxFileKind.None )
                {
                    var fileName = codeTab.TabId;

                    if ( !fileName.EndsWith( ".cs", StringComparison.Ordinal ) )
                    {
                        fileName += ".cs";
                    }

                    sandboxFiles.Add( new SandboxFile( fileName, codeTab.GetSandboxCode(), codeTab.SandboxFileKind ) );
                }
            }
            else if ( tab is CompareTab compareTab )
            {
                // Try currently requires that the code that is executed in Program.cs.
                var fileName = "Program.cs";

                sandboxFiles.Add( new SandboxFile( fileName, compareTab.GetSandboxCode(), SandboxFileKind.TargetCode ) );
            }
        }

        if ( canOpenInSandbox )
        {
            return new SandboxPayload( sandboxFiles ).ToCompressedString();
        }
        else
        {
            return null;
        }
    }
}