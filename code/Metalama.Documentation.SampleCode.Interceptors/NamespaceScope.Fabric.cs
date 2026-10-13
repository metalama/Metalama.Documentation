// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.IO;
using System.Linq;

namespace Doc.NamespaceScope.Billing
{
    // A namespace fabric intercepts only the calls in its namespace.
    // [<snippet Fabric>]
    internal class Fabric : NamespaceFabric
    {
        public override void AmendNamespace( INamespaceAmender amender )
        {
            var interceptor = typeof(AuditedFile).AsINamedType()
                .Methods.OfName( nameof(AuditedFile.ReadAllText) )
                .Single();

            amender.InterceptMethods(
                calls => calls
                    .Type( typeof(File) )
                    .Methods( nameof(File.ReadAllText) )
                    .Where( m => m.Parameters.Count == 1 )
                    .RedirectToExistingMethod( interceptor ) );
        }
    }
    // [<endsnippet Fabric>]
}

namespace Doc.NamespaceScope
{
    // The interceptor is outside the Billing namespace, so its own call to File.ReadAllText
    // is not intercepted.
    public static class AuditedFile
    {
        public static string ReadAllText( string path )
        {
            System.Console.WriteLine( $"Reading {Path.GetFileName( path )}." );

            return File.ReadAllText( path );
        }
    }
}
