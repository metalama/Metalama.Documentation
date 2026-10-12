// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.Invokers;
using System;
using System.Linq;

namespace Doc.LAMA0650.Error;

public class LogComparisonAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var describeMethod = meta.Target.Type.BaseType!.Methods.OfName( "Describe" ).Single();

        // Error: a base call is possible only on the current instance, not on 'other'.
        var otherDescription = describeMethod
            .WithObject( meta.Target.Parameters[0] )
            .WithOptions( InvokerOptions.Base )
            .Invoke();

        Console.WriteLine( $"Comparing with {otherDescription}." );

        return meta.Proceed();
    }
}
