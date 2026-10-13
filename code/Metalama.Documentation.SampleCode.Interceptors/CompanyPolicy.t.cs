using System.Net.Http;
using System.Threading.Tasks;
namespace Doc.CompanyPolicy;
// A project of the company that references the shared package.
public class WeatherService
{
  private readonly HttpClient _httpClient = new();
  public Task<string> GetForecastAsync(string city) => MetalamaInterceptors.HttpClient_GetStringAsync_Interceptor(this._httpClient, requestUri: $"https://weather.example.com/{city}");
}