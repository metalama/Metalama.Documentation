using System;
namespace Doc.LogSetters;
[LogSetters]
internal partial class Person
{
  private string? _name;
  public string? Name
  {
    get
    {
      return _name;
    }
    set
    {
      Console.WriteLine($"Setting Name = {value}");
      _name = value;
    }
  }
  private int _age;
  public int Age
  {
    get
    {
      return _age;
    }
    set
    {
      Console.WriteLine($"Setting Age = {value}");
      _age = value;
    }
  }
  // This property has no setter, so the aspect skips it.
  public string FullName => $"{Name} ({Age})";
}
