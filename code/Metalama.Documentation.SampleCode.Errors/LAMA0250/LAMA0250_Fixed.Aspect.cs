// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0250.Fixed;

public class SkipIfDisabledAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        if ( !FeatureFlags.IsReportingEnabled )
        {
            // Fixed: meta.Default returns the default value of the return type.
            return meta.Default( meta.Target.Method.ReturnType );
        }

        return meta.Proceed();
    }
}
