// This is public domain Metalama sample code.

using System.Net.Http;
using System.Threading.Tasks;

namespace Doc.LogHttpCalls;

[LogHttpCalls]
public class OrderService
{
    private readonly HttpClient _httpClient = new();

    public Task<string> GetOrderAsync( int id )
        => this._httpClient.GetStringAsync( $"https://api.example.com/orders/{id}" );

    public Task<string> GetCustomerAsync( string name )
        => this._httpClient.GetStringAsync( $"https://api.example.com/customers/{name}" );
}
