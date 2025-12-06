// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.Invokers;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Eligibility;
using System.Linq;

namespace Doc.TupleInterceptor;

public class Intercept : MethodAspect
{
    public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
    {
        builder.ReturnType().MustEqual( SpecialType.Void );
        builder.MustNotHaveRefOrOutParameter();
    }

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        base.BuildAspect( builder );

        // Create a tuple type for all parameters.
        var argsType = TypeFactory.CreateTupleType( builder.Target.Parameters );

        // Create the invoke method, which will be passed to the Interceptor.
        var invokeMethod = builder.WithDeclaringType()
            .IntroduceMethod(
                nameof(this.InvokeTemplate),
                args: new { argsType, originalMethod = builder.Target },
                buildMethod:
                m =>
                {
                    m.Name = builder.Target.Name + "Impl";
                    m.Parameters[0].Type = argsType;
                } )
            .Declaration;

        // Override the original method so that it invokes the Interceptor.
        builder.Override( nameof(this.OverrideTemplate), args: new { argsType, invokeMethod, T = argsType } );
    }

    // Template overriding the target method.
    [Template]
    private void OverrideTemplate<[CompileTime] T>( ITupleType argsType, IMethod invokeMethod )
    {
        // Pack all arguments into a tuple.
        var args = argsType.CreateCreateInstanceExpression( meta.Target.Parameters );

        // Call the interceptor.
        Interceptor.Intercept<T>( ExpressionFactory.Parse( invokeMethod.Name ).Value, (T) args.Value! );
    }

    // Template for the method called by the Interceptor.
    [Template]
    private void InvokeTemplate( dynamic args, IMethod originalMethod, ITupleType argsType )
    {
        // Unpack the tuple into an argument list. 
        var argsExpression = (IExpression) args;
        var arguments = argsType.TupleElements.Select( e => e.WithObject( argsExpression ) );
        
        // Invoke the original method.
        originalMethod.WithOptions( InvokerOptions.Base ).Invoke( arguments );
    }
}