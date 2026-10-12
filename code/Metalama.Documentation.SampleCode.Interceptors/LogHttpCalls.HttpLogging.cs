// This is public domain Metalama sample code.

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doc.LogHttpCalls;

public static class HttpLogging
{
    public static async Task<string> GetStringAsync( HttpClient client, string? requestUri )
    {
        Console.WriteLine( $"GET {requestUri}" );

        var result = await client.GetStringAsync( requestUri );

        Console.WriteLine( $"Received {result.Length} characters." );

        return result;
    }
}
