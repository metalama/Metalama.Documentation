// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0699.Fixed;

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

            // Fixed: meta.Proceed() is called only once, and the exception is rethrown.
            throw;
        }
    }
}
