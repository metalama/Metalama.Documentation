// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;
using Metalama.Framework.Engine.Utilities.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading.Tasks;

// [Log] executes before [MakeVirtual] and before [NotNull], but the order between [MakeVirtual] and [NotNull]
// isn't specified.
[assembly: AspectOrder( AspectOrderDirection.CompileTime, typeof(Doc.LAMA0042.Error.LogAttribute), typeof(Doc.LAMA0042.Error.MakeVirtualAttribute) )]
[assembly: AspectOrder( AspectOrderDirection.CompileTime, typeof(Doc.LAMA0042.Error.LogAttribute), typeof(Doc.LAMA0042.Error.NotNullAttribute) )]

namespace Doc.LAMA0042.Error;

// A low-level aspect, implemented by an aspect weaver.
[RequireAspectWeaver( "Doc.LAMA0042.Error.MakeVirtualWeaver" )]
internal class MakeVirtualAttribute : TypeAspect { }

[MetalamaPlugIn]
internal class MakeVirtualWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context ) => context.RewriteAspectTargetsAsync( new Rewriter() );

    private class Rewriter : SafeSyntaxRewriter
    {
        // Adds the virtual modifier to public methods.
        public override SyntaxNode? VisitMethodDeclaration( MethodDeclarationSyntax node )
            => node.Modifiers.Any( SyntaxKind.PublicKeyword ) && !node.Modifiers.Any( SyntaxKind.VirtualKeyword )
                ? node.AddModifiers( SyntaxFactory.Token( SyntaxKind.VirtualKeyword ).WithTrailingTrivia( SyntaxFactory.Space ) )
                : node;
    }
}

// A contract aspect, which has two layers.
internal class NotNullAttribute : ContractAspect
{
    public override void Validate( dynamic? value )
    {
        if ( value == null )
        {
            throw new ArgumentNullException( meta.Target.Parameter.Name );
        }
    }
}

// A high-level aspect, which has a single layer.
internal class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
