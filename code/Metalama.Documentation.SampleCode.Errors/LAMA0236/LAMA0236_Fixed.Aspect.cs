// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0236.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the field is introduced into the target type, where Logger is available at run time.
    [Introduce]
    private readonly Logger _logger = new Logger();

    public override dynamic? OverrideMethod()
    {
        this._logger.Write( $"Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
