// This is public domain Metalama sample code.

using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace Doc.LAMA0050.Fixed;

// The weaver is annotated with [MetalamaPlugIn], and its full name matches the name given to [RequireAspectWeaver].
[MetalamaPlugIn]
internal class VirtualizeWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context )
        => context.RewriteAspectTargetsAsync( new Rewriter() );

    private class Rewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration( MethodDeclarationSyntax node )
            => node.AddModifiers( SyntaxFactory.Token( SyntaxKind.VirtualKeyword ).WithTrailingTrivia( SyntaxFactory.ElasticSpace ) );
    }
}
