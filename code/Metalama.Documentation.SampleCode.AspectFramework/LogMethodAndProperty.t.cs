using System;
namespace Doc.LogMethodAndProperty;
internal class Foo
{
  [Log]
  public int Method(int a, int b)
  {
    Console.WriteLine("Entering Foo.Method(int, int)");
    try
    {
      return a + b;
    }
    finally
    {
      Console.WriteLine(" Leaving Foo.Method(int, int)");
    }
  }
  private int _property;
  [Log]
  public int Property
  {
    get
    {
      return _property;
    }
    set
    {
      Console.WriteLine("Assigning Foo.Property");
      _property = value;
    }
  }
  private string? _field;
  [Log]
  public string? Field
  {
    get
    {
      return _field;
    }
    set
    {
      Console.WriteLine("Assigning Foo.Field");
      _field = value;
    }
  }
}