// This is public domain Metalama sample code.

using System.Linq;
using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA1002.Error;

public class FlushLogAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var introduced = builder.IntroduceMethod( nameof(this.Flush) );

        // The scope is a method that the aspect introduces.
        builder.With( introduced.Declaration ).InterceptMethods(
            b => b
                .Type( typeof(LegacyLogger) )
                .Methods( "Write" )
                .RedirectToExistingMethod( typeof(Logger).AsINamedType().Methods.OfName( "Write" ).Single() ) );
    }

    [Template]
    public void Flush() => LegacyLogger.Write( "Flushed." );
}
