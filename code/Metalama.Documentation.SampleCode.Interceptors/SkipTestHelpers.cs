// This is public domain Metalama sample code.

using System;

namespace Doc.SkipTestHelpers;

[LogPaymentCalls]
public class CheckoutService
{
    public string Pay( decimal amount ) => PaymentGateway.Charge( amount );
}

[TestHelper]
[LogPaymentCalls]
public class FakeCheckout
{
    public string PayForTest() => PaymentGateway.Charge( 1 );
}

public static class PaymentGateway
{
    public static string Charge( decimal amount ) => $"ch_{amount}";
}

[AttributeUsage( AttributeTargets.Class )]
public class TestHelperAttribute : Attribute;
