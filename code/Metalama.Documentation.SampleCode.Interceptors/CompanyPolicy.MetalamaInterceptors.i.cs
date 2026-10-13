using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
internal static class MetalamaInterceptors
{
  internal static async Task<string> HttpClient_GetStringAsync_Interceptor(HttpClient receiver, string? requestUri)
  {
    var stopwatch = Stopwatch.StartNew();
    try
    {
      return await receiver.GetStringAsync(requestUri);
    }
    finally
    {
      Console.WriteLine($"HTTP call took {stopwatch.ElapsedMilliseconds} ms.");
    }
  }
}