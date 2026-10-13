// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.Comparers;
using System.Linq;

namespace Doc.LAMA0552.Fixed;

public class TraceEqualsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Override the strongly typed Equals method, which the record declares for its own type.
        var equals = builder.Target.Methods.OfName( "Equals" )
            .Single( m => m.Parameters[0].Type.Equals( builder.Target, TypeComparison.Default ) );

        builder.With( equals ).Override( nameof(this.OverrideEquals) );
    }

    [Template]
    private dynamic? OverrideEquals()
    {
        System.Console.WriteLine( "Comparing." );

        return meta.Proceed();
    }
}
