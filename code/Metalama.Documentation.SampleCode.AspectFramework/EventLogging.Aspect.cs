// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.EventLogging;

public class LogAttribute : OverrideEventAspect
{
    public override void OverrideAdd( dynamic value )
    {
        Console.WriteLine( $"Adding handler {((Delegate) value).Method}." );
        meta.Proceed();
    }

    public override void OverrideRemove( dynamic value )
    {
        Console.WriteLine( $"Removing handler {((Delegate) value).Method}." );
        meta.Proceed();
    }
}