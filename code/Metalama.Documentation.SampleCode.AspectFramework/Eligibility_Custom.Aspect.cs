// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;

namespace Doc.Eligibility_Custom;

internal class LogAttribute : OverrideMethodAspect
{
    public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
    {
        base.BuildEligibility( builder );

        // The aspect must not be offered on record classes.
        builder
            .DeclaringType()
            .MustSatisfy(
                t => !t.IsRecord,
                t => $"{t} must not be a record type" );
    }

    public override dynamic? OverrideMethod()
    {
        meta.This._logger.WriteLine( $"Executing {meta.Target.Method}" );

        return meta.Proceed();
    }
}