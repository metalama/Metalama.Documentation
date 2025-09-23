// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.SafeEvent_;

public class SafeEventAttribute : OverrideEventAspect
{
    public override void OverrideAdd( dynamic value )
    {
        meta.Proceed();
    }

    public override void OverrideRemove( dynamic value )
    {
        meta.Proceed();
    }

    public override void OverrideRaise( dynamic handler )
    {
        try
        {
            meta.Proceed();
        }
        catch ( Exception e )
        {
            Console.WriteLine( e );
            meta.Target.Event.RemoveMethod.Invoke( handler );
        }
    }
}