namespace Doc.DedupShared;
public class OrderService
{
  public string Pay(decimal amount) => MetalamaInterceptors.PaymentGateway_Charge_Interceptor(amount: amount);
  public string PayTwice(decimal amount) => MetalamaInterceptors.PaymentGateway_Charge_Interceptor(amount: amount) + MetalamaInterceptors.PaymentGateway_Charge_Interceptor(amount: amount);
}
public class SubscriptionService
{
  public string Renew(decimal fee) => MetalamaInterceptors.PaymentGateway_Charge_Interceptor(amount: fee);
}
public static class PaymentGateway
{
  public static string Charge(decimal amount) => $"ch_{amount}";
}