using System;
namespace Doc.LAMA0255.Fixed;
internal class CustomerRepository
{
  [LogFirstParameter]
  public string? FindCustomerId(string name)
  {
    Console.WriteLine("name");
    return null;
  }
}