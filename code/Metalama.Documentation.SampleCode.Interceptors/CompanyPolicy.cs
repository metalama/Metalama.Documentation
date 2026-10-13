// This is public domain Metalama sample code.

using System.Net.Http;
using System.Threading.Tasks;

namespace Doc.CompanyPolicy;

// A project of the company that references the shared package.
public class WeatherService
{
    private readonly HttpClient _httpClient = new();

    public Task<string> GetForecastAsync( string city )
        => this._httpClient.GetStringAsync( $"https://weather.example.com/{city}" );
}
