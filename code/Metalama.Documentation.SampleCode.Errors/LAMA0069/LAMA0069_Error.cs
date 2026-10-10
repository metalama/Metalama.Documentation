// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0069.Error;

internal class ServiceBase
{
    public void Ping() { }
}

internal class OrderService : ServiceBase
{
    public void PlaceOrder( int productId ) { }

    private class Fabric : TypeFabric
    {
        private static readonly DiagnosticDefinition<IMethod> _legacyMethod = new(
            "DOC001",
            Severity.Warning,
            "The method '{0}' is a legacy method and should not be used in new code." );

        public override void AmendType( ITypeAmender amender )
        {
            // Error: a type fabric can't report diagnostics on the members of the base class.
            amender.SelectMany( t => t.BaseType!.Methods ).ReportDiagnostic( m => _legacyMethod.WithArguments( m ) );
        }
    }
}
