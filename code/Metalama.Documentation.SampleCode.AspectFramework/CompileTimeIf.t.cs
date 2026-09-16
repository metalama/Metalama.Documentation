using System;
namespace Doc.CompileTimeIf;
internal class Foo
{
  [CompileTimeIf]
  public void InstanceMethod()
  {
    Console.WriteLine($"Invoking Foo.InstanceMethod() on instance {this.ToString()}.");
    Console.WriteLine("InstanceMethod");
  }
  [CompileTimeIf]
  public static void StaticMethod()
  {
    Console.WriteLine("Invoking Foo.StaticMethod()");
    Console.WriteLine("StaticMethod");
  }
}