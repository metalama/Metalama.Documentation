using System;
namespace Doc.LAMA0108.Fixed;
internal class CustomerService
{
  [LogNullArguments]
  public void Register(string? name, string? email)
  {
    var nullArgumentCount = 0;
    if (name == null)
    {
      nullArgumentCount++;
    }
    if (email == null)
    {
      nullArgumentCount++;
    }
    Console.WriteLine($"Register received {nullArgumentCount} null argument(s).");
  }
}