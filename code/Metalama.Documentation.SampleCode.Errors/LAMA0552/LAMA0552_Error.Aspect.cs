// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0552.Error;

public class TraceEqualsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: the compiler adds Equals(object) to every record, so it cannot be overridden.
        builder.IntroduceMethod( nameof(this.IntroducedEquals), whenExists: OverrideStrategy.Override );
    }

    [Template( Name = "Equals" )]
    public bool IntroducedEquals( object? other )
    {
        System.Console.WriteLine( "Comparing." );

        return meta.Proceed();
    }
}
