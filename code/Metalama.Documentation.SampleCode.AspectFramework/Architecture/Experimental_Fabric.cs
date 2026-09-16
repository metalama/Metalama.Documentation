// This is public domain Metalama sample code.

using Doc.Architecture.Experimental_Fabric.ExperimentalNamespace;
using Metalama.Extensions.Architecture;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.Architecture.Experimental_Fabric
{
    namespace ExperimentalNamespace
    {
        internal class Fabric : NamespaceFabric
        {
            public override void AmendNamespace( INamespaceAmender amender )
            {
                amender.SelectTypes()
                    .Where( t => t.Accessibility == Accessibility.Public )
                    .Experimental();
            }
        }

        public static class ExperimentalApi
        {
            public static void Foo() { }
        }
    }

    internal static class ProductionCode
    {
        public static void Dummy()
        {
            // This call is reported.
            ExperimentalApi.Foo();
        }
    }
}