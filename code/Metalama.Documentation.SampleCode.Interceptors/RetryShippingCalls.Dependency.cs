// This is public domain Metalama sample code.

using System;
using System.Threading.Tasks;

namespace Doc.RetryShippingCalls;

// A third-party SDK, compiled in another project. Its methods fail the first two times they are called.
public class ShippingClient
{
    private int _failures;

    public decimal GetQuote( string destination )
    {
        this.FailTwice();

        return destination == "US" ? 25m : 12m;
    }

    public async Task<string> GetTrackingStatusAsync( string trackingNumber )
    {
        await Task.Yield();
        this.FailTwice();

        return "Delivered";
    }

    public string CreateShipment( string destination ) => $"SHP-{destination}";

    private void FailTwice()
    {
        if ( this._failures++ % 3 < 2 )
        {
            throw new TimeoutException( "The shipping service did not respond." );
        }
    }
}
