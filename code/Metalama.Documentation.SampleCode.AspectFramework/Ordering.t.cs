using System;
namespace Doc.Ordering;
[Aspect1]
[Aspect2]
internal partial class Foo
{
  public static void SourceMethod()
  {
    Console.WriteLine("Executing Aspect1 on SourceMethod. Methods present before applying Aspect1: SourceMethod, IntroducedMethod2");
    Console.WriteLine("Executing Aspect2 on SourceMethod. Methods present before applying Aspect2: SourceMethod");
    Console.WriteLine("Method defined in source code.");
  }
  public static void IntroducedMethod1()
  {
    Console.WriteLine("Method introduced by Aspect1.");
  }
  public static void IntroducedMethod2()
  {
    Console.WriteLine("Executing Aspect1 on IntroducedMethod2. Methods present before applying Aspect1: SourceMethod, IntroducedMethod2");
    Console.WriteLine("Method introduced by Aspect2.");
  }
}
public static class Program
{
  public static void Main()
  {
    Console.WriteLine("Executing SourceMethod:");
    Foo.SourceMethod();
    Console.WriteLine("---");
    Console.WriteLine("Executing IntroducedMethod1:");
    Foo.IntroducedMethod1();
    Console.WriteLine("---");
    Console.WriteLine("Executing IntroducedMethod2:");
    Foo.IntroducedMethod2();
  }
}