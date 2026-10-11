// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.Invokers;
using System;
using System.Linq;

namespace Doc.LAMA0650.Fixed;

public class LogComparisonAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var describeMethod = meta.Target.Type.BaseType!.Methods.OfName( "Describe" ).Single();

        // Fixed: the final implementation can be invoked on any instance.
        var otherDescription = describeMethod
            .WithObject( meta.Target.Parameters[0] )
            .WithOptions( InvokerOptions.Final )
            .Invoke();

        Console.WriteLine( $"Comparing with {otherDescription}." );

        return meta.Proceed();
    }
}
