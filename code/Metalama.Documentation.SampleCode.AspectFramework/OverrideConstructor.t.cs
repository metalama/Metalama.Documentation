using System;
namespace Doc.OverrideConstructor;
[LogConstructors]
internal class SomeClass
{
  private readonly string _name;
  public SomeClass() : this("")
  {
    Console.WriteLine("Executing constructor SomeClass.SomeClass(): started");
    Console.WriteLine("Within constructor A.");
    Console.WriteLine("Executing constructor SomeClass.SomeClass(): completed");
  }
  public SomeClass(string name)
  {
    Console.WriteLine("Executing constructor SomeClass.SomeClass(string): started");
    this._name = name;
    Console.WriteLine("Within constructor B.");
    Console.WriteLine("Executing constructor SomeClass.SomeClass(string): completed");
  }
}
internal static class Program
{
  public static void Main()
  {
    _ = new SomeClass();
  }
}