// This is public domain Metalama sample code.

using System.Threading.Tasks;

namespace Doc.LAMA0254.Error;

internal class PriceService
{
    [Log]
    public async Task<decimal> GetPriceAsync( string productId )
    {
        await Task.Delay( 10 );

        return 42m;
    }
}
