using System;
namespace Doc.LAMA0223.Fixed;
internal class PriceCalculator
{
  [Log]
  public decimal GetPrice(int quantity)
  {
    Console.WriteLine("Calling GetPrice.");
    return quantity * 9.99m;
  }
}