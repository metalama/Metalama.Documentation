// This is public domain Metalama sample code.

namespace Doc.DedupShared;

public class OrderService
{
    public string Pay( decimal amount ) => PaymentGateway.Charge( amount );

    public string PayTwice( decimal amount )
        => PaymentGateway.Charge( amount ) + PaymentGateway.Charge( amount );
}

public class SubscriptionService
{
    public string Renew( decimal fee ) => PaymentGateway.Charge( fee );
}

public static class PaymentGateway
{
    public static string Charge( decimal amount ) => $"ch_{amount}";
}
