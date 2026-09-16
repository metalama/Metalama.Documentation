// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System.IO;

namespace BuildMetalamaDocumentation.Markdig.CompareFile;

internal class HtmlCompareFileInlineRenderer : HtmlObjectRenderer<CompareFileInline>
{
    protected override void Write( HtmlRenderer renderer, CompareFileInline obj )
    {
        if ( !File.Exists( obj.Src ) )
        {
            throw new FileNotFoundException( $"The file '{obj.Src}' does not exist." );
        }

        var name = Path.GetFileNameWithoutExtension( obj.Src ).ToLowerInvariant();
        var compareTab = new CompareTab( name, name, obj.Src );
        var content = compareTab.GetTabContent();

        renderer.WriteLine( content );
    }
}