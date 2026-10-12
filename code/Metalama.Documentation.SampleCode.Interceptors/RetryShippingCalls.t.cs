using System.Threading.Tasks;
namespace Doc.RetryShippingCalls;
public class ShipmentService
{
  private readonly ShippingClient _client = new();
  public decimal EstimateCost(string destination) => MetalamaInterceptors.ShippingClient_GetQuote_Interceptor(this._client, destination: destination);
  public Task<string> GetStatusAsync(string trackingNumber) => MetalamaInterceptors.ShippingClient_GetTrackingStatusAsync_Interceptor(this._client, trackingNumber: trackingNumber);
  public string Ship(string destination) => this._client.CreateShipment(destination);
}