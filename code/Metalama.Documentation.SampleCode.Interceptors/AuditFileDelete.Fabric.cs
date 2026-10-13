// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.IO;
using System.Linq;

namespace Doc.AuditFileDelete;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var interceptor = typeof(Audit).AsINamedType()
            .Methods.OfName( nameof(Audit.DeleteFile) )
            .Single();

        amender.InterceptMethods(
            calls => calls
                .Type( typeof(File) )
                .Methods( nameof(File.Delete) )
                .RedirectToExistingMethod( interceptor )
                // [<snippet WithArgs>]
                .WithArgs( new { category = "FileSystem" } ) );
                // [<endsnippet WithArgs>]
    }
}
