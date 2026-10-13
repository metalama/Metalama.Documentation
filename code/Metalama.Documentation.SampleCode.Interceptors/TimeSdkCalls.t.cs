using System.Threading.Tasks;
namespace Doc.TimeSdkCalls;
public class CheckoutService
{
  private readonly PaymentGateway _gateway = new();
  public string Pay(decimal amount) => MetalamaInterceptors.PaymentGateway_Charge_Interceptor(this._gateway, amount: amount, currency: "EUR");
  public Task CancelAsync(string chargeId) => MetalamaInterceptors.PaymentGateway_RefundAsync_Interceptor(this._gateway, chargeId: chargeId);
  public bool CanPay(decimal amount) => MetalamaInterceptors.PaymentGateway_GetBalance_Interceptor(this._gateway) >= amount;
}