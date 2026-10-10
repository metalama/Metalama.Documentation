using System;
namespace Doc.LAMA0256.Fixed;
internal class Customer
{
  public string? Name { get; set; }
  public string? Email { get; set; }
}
internal class CustomerService
{
  [LogProperties]
  public void Save(Customer customer)
  {
    var value = customer.Name;
    Console.WriteLine($"Name = {value}");
    var value_1 = customer.Email;
    Console.WriteLine($"Email = {value_1}");
  }
}