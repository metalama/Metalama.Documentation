// This is public domain Metalama sample code.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doc.LAMA0293.Fixed;

internal class StockService
{
    // Fixed: the aspect uses a dedicated template for async iterators.
    [Log]
    public async IAsyncEnumerable<decimal> GetPricesAsync( string symbol )
    {
        await Task.Yield();

        yield return 100.5m;
        yield return 101.25m;
    }
}
