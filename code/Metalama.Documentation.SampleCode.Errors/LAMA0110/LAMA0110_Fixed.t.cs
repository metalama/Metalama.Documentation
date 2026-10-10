using System;
namespace Doc.LAMA0110.Fixed;
internal class CustomerRepository
{
  [LogNullResult]
  public string? FindCustomerId(string name, string city)
  {
    string? result;
    result = null;
    if (result == null)
    {
      Console.WriteLine($"FindCustomerId returned null for name = {name}.");
      Console.WriteLine($"FindCustomerId returned null for city = {city}.");
    }
    return result;
  }
}