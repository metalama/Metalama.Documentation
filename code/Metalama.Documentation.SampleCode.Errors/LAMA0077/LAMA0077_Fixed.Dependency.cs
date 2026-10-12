// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace Doc.LAMA0077.Fixed;

// A low-level aspect, which requires the weaver VirtualizeWeaver.
[RequireAspectWeaver( "Doc.LAMA0077.Fixed.VirtualizeWeaver" )]
public class VirtualizeAttribute : TypeAspect { }

[MetalamaPlugIn]
public class VirtualizeWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context )
        => context.RewriteAspectTargetsAsync( new Rewriter() );

    private class Rewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration( MethodDeclarationSyntax node )
            => node.AddModifiers( SyntaxFactory.Token( SyntaxKind.VirtualKeyword ).WithTrailingTrivia( SyntaxFactory.ElasticSpace ) );
    }
}
