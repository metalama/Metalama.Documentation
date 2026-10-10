// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.LAMA0020.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: a nested type must be separated from its parent type with '+', not '.'.
        var loggerType = TypeFactory.GetType( "Doc.LAMA0020.Error.Logging.ConsoleLogger" );

        loggerType.Methods.OfName( "Write" ).Single().Invoke( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
