// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Markdig;
using Markdig.Renderers;

namespace BuildMetalamaDocumentation.Markdig.SingleFiles;

public class SingleFileInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<SingleFileInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new SingleFileInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlSingeFileInlineRenderer>();
        }
    }
}