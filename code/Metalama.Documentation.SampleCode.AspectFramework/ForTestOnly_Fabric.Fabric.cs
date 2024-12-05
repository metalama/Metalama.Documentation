// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Validation;
using System;

namespace Doc.ForTestOnly_Fabric
{
    namespace ValidatedNamespace
    {
        public class Fabric : NamespaceFabric
        {
            private static readonly DiagnosticDefinition<IDeclaration> _warning = new(
                "MY001",
                Severity.Warning,
                "'{0}' can only be invoked from a namespace that ends with '.Tests'." );

            public override void AmendNamespace( INamespaceAmender amender )
            {
                amender.ValidateInboundReferences(
                    this.ValidateReference,
                    ReferenceGranularity.Namespace );
            }

            private void ValidateReference( ReferenceValidationContext context )
            {
                if (
                    !Equals( context.Origin.Namespace, context.Destination.Declaration ) &&
                    !context.Origin.Namespace.FullName.EndsWith(
                        ".Tests",
                        StringComparison.Ordinal ) )
                {
                    context.Diagnostics.Report(
                        _warning.WithArguments( context.Destination.Declaration ) );
                }
            }
        }
    }
}