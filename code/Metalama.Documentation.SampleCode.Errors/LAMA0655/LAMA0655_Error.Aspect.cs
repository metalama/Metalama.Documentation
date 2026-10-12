// This is public domain Metalama sample code.

using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0655.Error;

public class LogEqualsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Override the Equals method that the C# compiler generates for the record.
        builder.IntroduceMethod(
            nameof(this.IntroducedEquals),
            whenExists: OverrideStrategy.Override,
            args: new { T = builder.Target } );

        // Override the accessors of the event.
        builder.With( builder.Target.Events.Single( e => e.Name == "Changed" ) )
            .OverrideAccessors( nameof(this.OverrideAccessor), nameof(this.OverrideAccessor) );
    }

    [Template( Name = "Equals" )]
    public bool IntroducedEquals<[CompileTime] T>( T? other )
    {
        Console.WriteLine( "Comparing two records." );

        return meta.Proceed();
    }

    [Template]
    public void OverrideAccessor()
    {
        Console.WriteLine( "Accessing the event." );
    }
}
