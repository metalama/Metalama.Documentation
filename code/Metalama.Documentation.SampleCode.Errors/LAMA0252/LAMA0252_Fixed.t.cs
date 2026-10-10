using System;
namespace Doc.LAMA0252.Fixed;
[Describe]
internal partial class Customer
{
  public string? Name { get; set; }
  public void Describe()
  {
    Console.WriteLine("This is a Customer.");
  }
}