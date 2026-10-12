// This is public domain Metalama sample code.

using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA1002.Fixed;

public class FlushLogAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.Flush) );

        // The scope is the target type, which exists in the source code.
        builder.InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() ) );
    }

    [Template]
    public void Flush() => LegacyLogger.Write( "Flushed." );
}
