// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0052.Error
{
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}
