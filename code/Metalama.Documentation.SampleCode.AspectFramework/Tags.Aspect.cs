// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.Tags;

internal class TagsAspect : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override(
            nameof(this.OverrideMethod),
            tags: new { ParameterCount = builder.Target.Parameters.Count } );
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        Console.WriteLine( $"This method has {meta.Tags["ParameterCount"]} parameters." );

        return meta.Proceed();
    }
}