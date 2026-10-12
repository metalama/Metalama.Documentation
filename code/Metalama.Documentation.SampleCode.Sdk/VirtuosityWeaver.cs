// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace Metalama.Community.Virtuosity;

[MetalamaPlugIn]
public sealed class VirtuosityWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context ) => context.RewriteAspectTargetsAsync( new Rewriter() );

    private sealed class Rewriter : CSharpSyntaxRewriter
    {
        private static readonly SyntaxKind[] _forbiddenModifiers = { StaticKeyword, SealedKeyword, VirtualKeyword, OverrideKeyword };

        private static readonly SyntaxKind[] _requiredModifiers = { PublicKeyword, ProtectedKeyword, InternalKeyword };

        private static SyntaxTokenList ModifyModifiers( SyntaxTokenList modifiers )
        {
            // Add the virtual modifier.
            if ( !_forbiddenModifiers.Any( modifier => modifiers.Any( modifier ) )
                 && _requiredModifiers.Any( modifier => modifiers.Any( modifier ) ) )
            {
                modifiers = modifiers.Add(
                    SyntaxFactory.Token( VirtualKeyword )
                        .WithTrailingTrivia( SyntaxFactory.ElasticSpace ) );
            }

            return modifiers;
        }

        public override SyntaxNode VisitMethodDeclaration( MethodDeclarationSyntax node ) => node.WithModifiers( ModifyModifiers( node.Modifiers ) );
    }
}