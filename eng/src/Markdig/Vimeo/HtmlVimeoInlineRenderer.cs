// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace BuildMetalamaDocumentation.Markdig.Vimeo;

public class HtmlVimeoInlineRenderer : HtmlObjectRenderer<VimeoInline>
{
    protected override void Write( HtmlRenderer renderer, VimeoInline obj ) => renderer.WriteLine( $"<div class='vimeo' data-id='{obj.Id}'></div>" );
}