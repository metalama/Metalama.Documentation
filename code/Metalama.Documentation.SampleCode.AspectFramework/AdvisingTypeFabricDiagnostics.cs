// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.AdvisingTypeFabricDiagnostics;

public partial class MyClass
{
    public string? Name { get; set; }

    public override string ToString() => "MyClass";

    private class Fabric : TypeFabric
    {
        private static readonly DiagnosticDefinition<INamedType> _warning = new(
            "MY001",
            Severity.Warning,
            "The type '{0}' should have a 'Name' property." );

        [Template]
        public string ToStringTemplate()
        {
            return $"{meta.Target.Type.Name} (advised by fabric)";
        }

        public override void AmendType( ITypeAmender amender )
        {
            // Report a warning if the type does not have a 'Name' property.
            if ( !amender.Type.Properties.OfName( "Name" ).Any() )
            {
                amender.Diagnostics.Report( _warning.WithArguments( amender.Type ) );
            }

            // Override ToString directly on the amender using the With method.
            amender.With( amender.Type.Methods.OfName( "ToString" ).Single() )
                .Override( nameof(this.ToStringTemplate) );
        }
    }
}
