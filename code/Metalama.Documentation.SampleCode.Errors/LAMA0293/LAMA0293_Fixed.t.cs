using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Doc.LAMA0293.Fixed;
internal class StockService
{
  // Fixed: the aspect uses a dedicated template for async iterators.
  [Log]
  public async IAsyncEnumerable<decimal> GetPricesAsync(string symbol)
  {
    Console.WriteLine("Enumerating StockService.GetPricesAsync(string).");
    await foreach (var item in this.GetPricesAsync_Source(symbol))
    {
      yield return item;
    }
    Console.WriteLine("StockService.GetPricesAsync(string) completed.");
  }
  private async IAsyncEnumerable<decimal> GetPricesAsync_Source(string symbol)
  {
    await Task.Yield();
    yield return 100.5m;
    yield return 101.25m;
  }
}