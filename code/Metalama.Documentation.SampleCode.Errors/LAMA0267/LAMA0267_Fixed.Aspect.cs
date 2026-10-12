// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Linq;

namespace Doc.LAMA0267.Fixed;

public class LogCategoryAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var categories = new[] { "Orders", "Billing" };

        // Fixed: a lambda expression invokes the extension method.
        Func<string> getCategory = () => categories.First();

        Console.WriteLine( $"[{getCategory()}] Executing {meta.Target.Method.Name}." );

        return meta.Proceed();
    }
}
