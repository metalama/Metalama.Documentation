// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.InheritedTypeLevel;

[Inheritable]
internal class InheritedAspectAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var method in builder.Target.Methods )
        {
            builder.With( method ).Override( nameof(this.MethodTemplate) );
        }
    }

    [Template]
    private dynamic? MethodTemplate()
    {
        Console.WriteLine( "Hacked!" );

        return meta.Proceed();
    }
}