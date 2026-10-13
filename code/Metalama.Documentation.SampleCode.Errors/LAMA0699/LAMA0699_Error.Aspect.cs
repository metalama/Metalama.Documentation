// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0699.Error;

public class LogConstructorErrorsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var constructor in builder.Target.Constructors )
        {
            builder.With( constructor ).Override( nameof(this.OverrideConstructor) );
        }
    }

    [Template]
    public void OverrideConstructor()
    {
        try
        {
            meta.Proceed();
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{meta.Target.Constructor} failed: {e.Message}" );

            // Error: retrying with a second call to meta.Proceed() makes the constructor impossible to inline.
            meta.Proceed();
        }
    }
}
