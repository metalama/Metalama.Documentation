using System;
namespace Doc.LAMA0101.Fixed;
internal class CustomerService
{
  [LogEntry]
  public void Register(string name, string email)
  {
    Console.WriteLine("Entering Register( name, email ).");
  }
}