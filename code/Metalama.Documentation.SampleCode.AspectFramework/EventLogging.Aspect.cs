// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.EventLogging;

public class LogAttribute : OverrideEventAspect
{
    public override void OverrideAdd( dynamic handler )
    {
        Console.WriteLine( $"Adding handler {((Delegate) handler).Method}." );
        meta.Proceed();
    }

    public override void OverrideRemove( dynamic handler )
    {
        Console.WriteLine( $"Removing handler {((Delegate) handler).Method}." );
        meta.Proceed();
    }
}