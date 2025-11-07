// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.CallAfter;

public class CallAfterAttribute : OverrideMethodAspect
{
    private readonly string _methodName;

    public CallAfterAttribute( string methodName ) 
    {
        this._methodName = methodName;
    }

    public override dynamic? OverrideMethod()
    {
        // Execute the method.
        var result = meta.Proceed();
        
        // Call the after method.
        var method = meta.Target.Type.AllMethods
            .OfName( this._methodName )
            .Single( p => p.Parameters.Count == 0 );

        method.Invoke();

        return result;
    }
}