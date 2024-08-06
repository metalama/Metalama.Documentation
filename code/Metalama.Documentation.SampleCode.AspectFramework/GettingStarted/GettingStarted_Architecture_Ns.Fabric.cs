﻿// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Fabrics;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;

namespace Doc.GettingStarted_Architecture_Ns
{
    namespace VerifiedNamespace
    {
        internal class Fabric : NamespaceFabric
        {
            public override void AmendNamespace( INamespaceAmender amender )
            {             
                amender.Verify()
                .CanOnlyBeUsedFrom( r => r.CurrentNamespace() );
            }
        }
    }
}
