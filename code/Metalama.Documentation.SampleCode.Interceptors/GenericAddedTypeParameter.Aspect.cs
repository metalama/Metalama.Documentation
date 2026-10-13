// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.GenericAddedTypeParameter;

public class TraceLoggingAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            calls => calls
                .Type( typeof(Log) )
                .Methods( nameof(Log.Write) )
                .RedirectToSynthesizedMethod( nameof(this.Trace) )
                .Configure(
                    method =>
                    {
                        // Make the parameter generic, and pass the type of the argument
                        // of each call site.
                        // [<snippet Configure>]
                        var typeParameter = method.Method.AddTypeParameter( "TValue" );
                        method.Method.Parameters[0].Type = typeParameter;
                        method.SetTypeArgument( typeParameter, method.Context.Arguments[0].Type! );
                        // [<endsnippet Configure>]
                    } ) );

    [Template]
    public dynamic? Trace()
    {
        var valueType = meta.Target.Method.TypeParameters[0];
        Console.WriteLine( $"Logging a {valueType.ToTypeOfExpression().Value!.Name}." );

        return meta.Proceed();
    }
}
