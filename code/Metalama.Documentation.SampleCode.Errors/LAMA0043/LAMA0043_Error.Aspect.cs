// This is public domain Metalama sample code.

using Metalama.Extensions.CodeFixes;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA0043.Error;

[AttributeUsage( AttributeTargets.Method )]
public class NotLoggedAttribute : Attribute { }

public class LogAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        foreach ( var method in builder.Target.Methods )
        {
            if ( method.Attributes.Any( a => a.Type.IsConvertibleTo( typeof(NotLoggedAttribute) ) ) )
            {
                continue;
            }

            builder.With( method ).Override( nameof(this.LogMethod) );

            // Suggest a code fix without reporting a visible diagnostic.
            builder.Diagnostics.Suggest(
                CodeFixFactory.AddAttribute( method, typeof(NotLoggedAttribute), "Exclude from logging" ),
                method );
        }
    }

    [Template]
    public dynamic? LogMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
