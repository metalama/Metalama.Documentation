using System;
namespace Doc.LAMA0280.Fixed;
internal class CustomerService
{
  [LogNullArguments]
  public void Register(string? name, string? email)
  {
    var nullCount = 0;
    if (name == null)
    {
      nullCount++;
    }
    if (email == null)
    {
      nullCount++;
    }
    Console.WriteLine($"Register received {nullCount} null argument(s).");
  }
}