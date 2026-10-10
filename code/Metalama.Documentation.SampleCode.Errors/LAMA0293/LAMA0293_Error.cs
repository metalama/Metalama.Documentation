// This is public domain Metalama sample code.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doc.LAMA0293.Error;

internal class StockService
{
    // Error: the default template wraps the async iterator in a try-catch block.
    [Log]
    public async IAsyncEnumerable<decimal> GetPricesAsync( string symbol )
    {
        await Task.Yield();

        yield return 100.5m;
        yield return 101.25m;
    }
}
