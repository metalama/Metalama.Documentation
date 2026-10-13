// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Linq;

namespace Doc.LAMA0289.Fixed;

public class LogTokenAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var tokenParameter = meta.Target.Parameters.FirstOrDefault( p => p.Name == "token" );

        // Fixed: a compile-time 'if' statement checks for null instead of the '?.' operator.
        if ( tokenParameter != null )
        {
            var normalizedToken = tokenParameter.Value?.ToLower();
            Console.WriteLine( $"Normalized token: {normalizedToken}" );
        }

        return meta.Proceed();
    }
}
