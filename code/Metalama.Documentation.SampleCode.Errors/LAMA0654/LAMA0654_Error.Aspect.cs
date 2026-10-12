// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0654.Error;

public class LogEqualsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Override the Equals method that the C# compiler generates for the record.
        builder.IntroduceMethod(
            nameof(this.IntroducedEquals),
            whenExists: OverrideStrategy.Override,
            args: new { T = builder.Target } );
    }

    [Template( Name = "Equals" )]
    public bool IntroducedEquals<[CompileTime] T>( T? other )
    {
        Console.WriteLine( "Comparing two records." );

        return meta.Proceed();
    }
}
