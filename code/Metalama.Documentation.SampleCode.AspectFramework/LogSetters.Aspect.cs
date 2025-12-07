// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LogSetters;

internal class LogSettersAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var property in builder.Target.Properties )
        {
            // Only override properties that have a setter.
            if ( property.Writeability != Writeability.None )
            {
                builder.With( property ).OverrideAccessors(
                    null,
                    nameof(this.SetterTemplate) );
            }
        }
    }

    [Template]
    private void SetterTemplate( dynamic? value )
    {
        Console.WriteLine(
            $"Setting {meta.Target.FieldOrProperty.Name} = {value}" );

        meta.Proceed();
    }
}
