// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Diagnostics;

namespace Doc.Profile;

public class ProfileAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var sw = Stopwatch.StartNew();

        try
        {
            return meta.Proceed();
        }
        finally
        {
            var name = meta.Target.Method.ToDisplayString();
            Console.WriteLine( $"{name} executed in {sw.ElapsedMilliseconds} ms." );
        }
    }
}