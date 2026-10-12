// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0229.Error;

internal class OrderService
{
    [Log]
    public void PlaceOrder( string product, int quantity ) { }

    // Error: an aspect class can't be nested in a run-time class.
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}
