// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.LAMA0020.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the reflection name of a nested type uses '+' as the separator.
        var loggerType = TypeFactory.GetType( "Doc.LAMA0020.Fixed.Logging+ConsoleLogger" );

        loggerType.Methods.OfName( "Write" ).Single().Invoke( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
