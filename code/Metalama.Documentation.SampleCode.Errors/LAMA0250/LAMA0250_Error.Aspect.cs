// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0250.Error;

public class SkipIfDisabledAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        if ( !FeatureFlags.IsReportingEnabled )
        {
            // Error: default(dynamic) is not allowed in a template.
            return default(dynamic);
        }

        return meta.Proceed();
    }
}
