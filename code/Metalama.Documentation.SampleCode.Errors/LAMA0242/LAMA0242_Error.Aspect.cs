// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0242.Error;

public class LogCustomerAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: the run-time 'as' operator is applied to the compile-time IParameter, not to the argument value.
        var customer = meta.Target.Parameters[0] as Customer;

        return meta.Proceed();
    }
}
