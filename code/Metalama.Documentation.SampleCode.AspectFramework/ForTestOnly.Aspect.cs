// This is public domain Metalama sample code.

using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System;

namespace Doc.ForTestOnly;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct |
    AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property |
    AttributeTargets.Event )]
public class ForTestOnlyAttribute : Attribute, IAspect<IMember>
{
    private static readonly DiagnosticDefinition<IDeclaration> _warning = new(
        "MY001",
        Severity.Warning,
        "'{0}' can only be invoked from a namespace that ends with Tests." );

    public void BuildAspect( IAspectBuilder<IMember> builder )
    {
        builder.Outbound.ValidateInboundReferences(
            this.ValidateReference,
            ReferenceGranularity.Namespace );
    }

    private void ValidateReference( ReferenceValidationContext context )
    {
        if ( !context.Origin.Namespace.FullName.EndsWith( ".Tests", StringComparison.Ordinal ) )
        {
            context.Diagnostics.Report(
                r => r.OriginDeclaration.IsContainedIn( context.Destination.Type )
                    ? null
                    : _warning.WithArguments( context.Destination.Namespace ) );
        }
    }
}