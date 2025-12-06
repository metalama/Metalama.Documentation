// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LogMethodAndProperty;

[AttributeUsage( AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property )]
public class LogAttribute : Aspect, IAspect<IMethod>, IAspect<IFieldOrProperty>
{
    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.OverrideMethod) );
    }

    public void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
    {
        builder.Override( nameof(this.OverrideFieldOrProperty) );
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        Console.WriteLine( "Entering " + meta.Target.Method.ToDisplayString() );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( " Leaving " + meta.Target.Method.ToDisplayString() );
        }
    }

    [Template]
    private dynamic? OverrideFieldOrProperty
    {
        get => meta.Proceed();

        set
        {
            Console.WriteLine( "Assigning " + meta.Target.FieldOrProperty.ToDisplayString() );
            meta.Proceed();
        }
    }
}