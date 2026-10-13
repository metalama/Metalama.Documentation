using System.Net.Http;
using System.Threading.Tasks;
namespace Doc.LogHttpCalls;
[LogHttpCalls]
public class OrderService
{
  private readonly HttpClient _httpClient = new();
  public Task<string> GetOrderAsync(int id) => HttpLogging.GetStringAsync(this._httpClient, $"https://api.example.com/orders/{id}");
  public Task<string> GetCustomerAsync(string name) => HttpLogging.GetStringAsync(this._httpClient, $"https://api.example.com/customers/{name}");
}