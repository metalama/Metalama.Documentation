// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;
using System.Threading;

namespace Doc.FakeSleep;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var interceptor = typeof(VirtualClock).AsINamedType().Methods.OfName( nameof(VirtualClock.Sleep) ).Single();

        // [<snippet Registration>]
        amender.InterceptMethods(
            calls => calls
                .Type( typeof(Thread) )
                .Methods( nameof(Thread.Sleep) )
                .Where( m => m.Parameters is [{ Type.SpecialType: SpecialType.Int32 }] )
                .RedirectToExistingMethod( interceptor ) );
        // [<endsnippet Registration>]
    }
}
