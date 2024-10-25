// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.PrintFieldValues;

internal class PrintFieldValuesAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties )
        {
            if ( fieldOrProperty is { IsImplicitlyDeclared: false, IsAutoPropertyOrField: true } )
            {
                var value = fieldOrProperty.Value;
                Console.WriteLine( $"{fieldOrProperty.Name}={value}" );
            }
        }

        return meta.Proceed();
    }
}