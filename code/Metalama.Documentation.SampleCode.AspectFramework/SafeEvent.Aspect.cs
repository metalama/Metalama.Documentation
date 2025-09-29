// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.SafeEvent_;

public class SafeEventAttribute : OverrideEventAspect
{
    public override dynamic? OverrideInvoke( dynamic handler )
    {
        try
        {
            return meta.Proceed();
        }
        catch ( Exception e )
        {
            // Log the error.
            Console.WriteLine( e );

            // Remove the faulted event handler.
            meta.Target.Event.Remove( handler );

            return null;
        }
    }
}