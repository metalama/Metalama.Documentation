// This is public domain Metalama sample code.

using System.Threading.Tasks;

namespace Doc.RetryShippingCalls;

public class ShipmentService
{
    private readonly ShippingClient _client = new();

    public decimal EstimateCost( string destination ) => this._client.GetQuote( destination );

    public Task<string> GetStatusAsync( string trackingNumber )
        => this._client.GetTrackingStatusAsync( trackingNumber );

    public string Ship( string destination ) => this._client.CreateShipment( destination );
}
