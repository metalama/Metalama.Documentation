// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.CreateDelegateExpression;

public class RegisterCallbackAttribute : OverrideMethodAspect
{
    private readonly string _callbackMethodName;

    public RegisterCallbackAttribute( string callbackMethodName )
    {
        this._callbackMethodName = callbackMethodName;
    }

    public override dynamic? OverrideMethod()
    {
        var result = meta.Proceed();

        // Find the callback method and create a delegate expression for it.
        var callbackMethod = meta.Target.Type.AllMethods
            .OfName( this._callbackMethodName )
            .Single( m => m.Parameters.Count == 1 );

        Action<string> callback = callbackMethod.CreateDelegateExpression().Value!;

        // Use the delegate.
        callback.Invoke( "Operation completed." );

        return result;
    }
}
