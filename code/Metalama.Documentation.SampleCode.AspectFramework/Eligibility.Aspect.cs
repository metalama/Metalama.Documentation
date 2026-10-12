// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;

namespace Doc.Eligibility;

internal class LogAttribute : OverrideMethodAspect
{
    public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
    {
        base.BuildEligibility( builder );

        // The aspect must not be offered to non-static methods because it uses a static field 'logger'.
        builder.MustNotBeStatic();
    }

    public override dynamic? OverrideMethod()
    {
        meta.This._logger.WriteLine( $"Executing {meta.Target.Method}" );

        return meta.Proceed();
    }
}