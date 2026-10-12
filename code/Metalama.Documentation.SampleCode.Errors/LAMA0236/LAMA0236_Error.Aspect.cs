// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0236.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Error: the aspect class cannot hold an instance of the run-time-only Logger class.
    private readonly Logger _logger = new Logger();

    public override dynamic? OverrideMethod()
    {
        this._logger.Write( $"Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
