// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0229.Fixed;

internal class OrderService
{
    [Log]
    public void PlaceOrder( string product, int quantity ) { }
}

// Fixed: the aspect class is declared at the namespace level.
public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
