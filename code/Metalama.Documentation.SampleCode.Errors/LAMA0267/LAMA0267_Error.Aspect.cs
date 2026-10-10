// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Linq;

namespace Doc.LAMA0267.Error;

public class LogCategoryAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var categories = new[] { "Orders", "Billing" };

        // Error: First is an extension method, so it cannot be converted to a delegate with a method group.
        Func<string> getCategory = categories.First;

        Console.WriteLine( $"[{getCategory()}] Executing {meta.Target.Method.Name}." );

        return meta.Proceed();
    }
}
