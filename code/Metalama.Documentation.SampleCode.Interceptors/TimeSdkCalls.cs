// This is public domain Metalama sample code.

using System.Threading.Tasks;

namespace Doc.TimeSdkCalls;

public class CheckoutService
{
    private readonly PaymentGateway _gateway = new();

    public string Pay( decimal amount ) => this._gateway.Charge( amount, "EUR" );

    public Task CancelAsync( string chargeId ) => this._gateway.RefundAsync( chargeId );

    public bool CanPay( decimal amount ) => this._gateway.GetBalance() >= amount;
}
