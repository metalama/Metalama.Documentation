// This is public domain Metalama sample code.

using Doc.GettingStarted_Architecture_Ns.VerifiedNamespace;

namespace Doc.GettingStarted_Architecture_Ns
{
    namespace VerifiedNamespace
    {
        internal class Foo { }

        internal class AllowedInheritor : Foo { }
    }

    namespace OtherNamespace
    {
        internal class ForbiddenInheritor : Foo { }
    }
}