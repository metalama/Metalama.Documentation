// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Markdig;
using Markdig.Renderers;

namespace BuildMetalamaDocumentation.Markdig.AspectTests;

public class AspectTestInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<AspectTestInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new AspectTestInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlAspectTestInlineRenderer>();
        }
    }
}