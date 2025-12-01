// This is public domain Metalama sample code.

using System;

namespace Doc.NamespaceFabric_
{
    namespace TargetNamespace
    {
        internal class TargetClass
        {
            public void TargetMethod()
            {
                Console.WriteLine( "Executing TargetMethod" );
            }
        }
    }

    namespace OtherNamespace
    {
        internal class OtherClass
        {
            public void OtherMethod()
            {
                Console.WriteLine( "Executing OtherMethod" );
            }
        }
    }
}
