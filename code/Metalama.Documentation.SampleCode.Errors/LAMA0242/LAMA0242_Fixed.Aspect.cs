// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0242.Fixed;

public class LogCustomerAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the 'as' operator is applied to the run-time value of the parameter.
        var customer = meta.Target.Parameters[0].Value as Customer;

        if ( customer != null )
        {
            Console.WriteLine( $"Customer {customer.Name} has {customer.LoyaltyPoints} loyalty points." );
        }

        return meta.Proceed();
    }
}
