using System;
namespace Doc.LAMA0509.Fixed;
// Fixed: the aspect uses OverrideStrategy.Override, so it overrides the existing Describe() method.
[Describe]
internal partial class Product
{
  public string Describe()
  {
    Console.WriteLine("Describing the object.");
    return "Product";
  }
}