// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0069.Fixed;

internal class ServiceBase
{
    public void Ping() { }

    private class Fabric : TypeFabric
    {
        private static readonly DiagnosticDefinition<IMethod> _legacyMethod = new(
            "DOC001",
            Severity.Warning,
            "The method '{0}' is a legacy method and should not be used in new code." );

        public override void AmendType( ITypeAmender amender )
        {
            // Fixed: the fabric is moved to the type that declares the methods.
            amender.SelectMany( t => t.Methods ).ReportDiagnostic( m => _legacyMethod.WithArguments( m ) );
        }
    }
}

internal class OrderService : ServiceBase
{
    public void PlaceOrder( int productId ) { }
}
