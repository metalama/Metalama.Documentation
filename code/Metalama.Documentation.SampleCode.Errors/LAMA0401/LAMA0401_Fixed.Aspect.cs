// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA0401.Fixed;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        var category = "Default";

        var categoryAttribute = builder.Target.Attributes.FirstOrDefault( a => a.Type.Name == "LogCategoryAttribute" );

        // Instantiates the [LogCategory] attribute at compile time.
        if ( categoryAttribute != null && categoryAttribute.TryConstruct( builder.Diagnostics, out var instance ) )
        {
            category = instance.ToString() ?? category;
        }

        builder.Override( nameof(this.OverrideMethod), args: new { category } );
    }

    [Template]
    public dynamic? OverrideMethod( [CompileTime] string category )
    {
        Console.WriteLine( $"[{category}] Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
