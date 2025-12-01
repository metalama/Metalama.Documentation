// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System;
using System.IO;
using System.Text.Json;

namespace BuildMetalamaDocumentation.Markdig.SingleFiles;

public class HtmlSingeFileInlineRenderer : HtmlObjectRenderer<SingleFileInline>
{

    protected override void Write( HtmlRenderer renderer, SingleFileInline obj )
    {
        var name = Path.GetFileNameWithoutExtension( obj.Src );

        var tab = obj.ShowTransformed
            ? new TransformedSingleFileCodeTab(
                Path.GetFileNameWithoutExtension( obj.Src ),
                obj.Src,
                "" )
            : new CodeTab( name, obj.Src, SandboxFileKind.ExtraCode, obj.Marker, obj.Member );

        renderer.WriteLine( "<div class='single-file'>" );
        renderer.WriteLine( tab.GetTabContent( false ) );

        // Emit JSON-LD block with plain code for AI discoverability.
        this.RenderJsonLd( renderer, obj, name );

        renderer.WriteLine( "</div>" );
    }

    private void RenderJsonLd( HtmlRenderer renderer, SingleFileInline obj, string name )
    {
        if ( !File.Exists( obj.Src ) )
        {
            return;
        }

        var plainCode = GetPlainCode( obj );

        if ( string.IsNullOrEmpty( plainCode ) )
        {
            return;
        }

        var jsonLd = new
        {
            @context = "https://schema.org",
            @type = "SoftwareSourceCode",
            name,
            programmingLanguage = "C#",
            codeSampleType = "code snippet",
            description = "Metalama code example.",
            file = new
            {
                name = Path.GetFileName( obj.Src ),
                marker = obj.Marker,
                member = obj.Member,
                content = plainCode
            }
        };

        var json = JsonSerializer.Serialize( jsonLd, new JsonSerializerOptions { WriteIndented = false } );

        renderer.WriteLine( $"<script type=\"application/ld+json\">{json}</script>" );
    }

    private static string? GetPlainCode( SingleFileInline obj )
    {
        var lines = File.ReadAllLines( obj.Src );

        // If a marker is specified, extract only the marked snippet.
        if ( !string.IsNullOrEmpty( obj.Marker ) )
        {
            return CodeContentHelper.ExtractSnippet( lines, obj.Marker );
        }

        // Otherwise, return the full file content (minus leading comment lines).
        return CodeContentHelper.ProcessCodeContent( lines );
    }
}