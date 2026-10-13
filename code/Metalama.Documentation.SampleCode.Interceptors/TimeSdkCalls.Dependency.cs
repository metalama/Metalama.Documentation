// This is public domain Metalama sample code.

using System.Threading.Tasks;

namespace Doc.TimeSdkCalls;

// A third-party SDK, compiled in another project.
public class PaymentGateway
{
    public string Charge( decimal amount, string currency ) => $"ch_{amount}_{currency}";

    public async Task RefundAsync( string chargeId ) => await Task.Yield();

    public decimal GetBalance() => 1000m;
}
