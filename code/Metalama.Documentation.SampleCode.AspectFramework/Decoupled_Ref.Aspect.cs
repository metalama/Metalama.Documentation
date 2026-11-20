﻿// This is public domain Metalama sample code.

using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.Decoupled_Ref;

internal class LogAspect : IAspect<IMethod>, IAspect<IProperty>
{
    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override(
            nameof(this.MethodTemplate),
            args: new { category = GetCategory( builder.Target ) } );
    }

    public void BuildAspect( IAspectBuilder<IProperty> builder )
    {
        builder.OverrideAccessors(
            null,
            nameof(this.MethodTemplate),
            args: new { category = GetCategory( builder.Target ) } );
    }

    // Reads the attribute data from the declaration to which the aspect is applied.
    private static string GetCategory( IDeclaration declaration )
    {
        var attribute = declaration.Attributes.OfAttributeType( typeof(LogAttribute) )
            .Single();

        return attribute.GetArgumentValue( nameof(LogAttribute.Category), "default" )!;
    }

    [Template]
    public dynamic? MethodTemplate( [CompileTime] string category )
    {
        Console.WriteLine( $"[{category}] Executing {meta.Target.Method}" );

        return meta.Proceed();
    }
}
