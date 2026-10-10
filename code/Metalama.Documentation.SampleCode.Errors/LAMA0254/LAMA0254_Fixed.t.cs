using System;
using System.Threading.Tasks;
namespace Doc.LAMA0254.Fixed;
internal class PriceService
{
  [Log]
  public async Task<decimal> GetPriceAsync(string productId)
  {
    Console.WriteLine("Starting GetPriceAsync.");
    var result = await GetPriceAsync_Source(productId).ConfigureAwait(false);
    return result;
  }
  private async Task<decimal> GetPriceAsync_Source(string productId)
  {
    await Task.Delay(10);
    return 42m;
  }
}