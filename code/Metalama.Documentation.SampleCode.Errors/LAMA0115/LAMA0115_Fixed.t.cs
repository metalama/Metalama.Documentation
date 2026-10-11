using System;
namespace Doc.LAMA0115.Fixed;
internal class Customer
{
  private string? _name;
  // Fixed: the template of [LogRead] uses meta.Target.FieldOrProperty.
  [LogRead]
  public string? Name
  {
    get
    {
      Console.WriteLine("Reading Name.");
      return _name;
    }
    set
    {
      _name = value;
    }
  }
}