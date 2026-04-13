using System;
namespace Doc.InsertParameter;
[IntroduceGreet]
internal partial class MyClass
{
  public void Greet(string firstName, string lastName, string greeting = "Hello")
  {
    Console.WriteLine(greeting);
  }
}
