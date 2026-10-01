using System;
namespace Doc.InheritedTypeLevel;
[InheritedAspect]
internal class BaseClass
{
  public void Method1()
  {
    Console.WriteLine("Hacked!");
  }
  public virtual void Method2()
  {
    Console.WriteLine("Hacked!");
  }
}
internal class DerivedClass : BaseClass
{
  public override void Method2()
  {
    Console.WriteLine("Hacked!");
    base.Method2();
  }
  public void Method3()
  {
    Console.WriteLine("Hacked!");
  }
}
internal class DerivedTwiceClass : DerivedClass
{
  public override void Method2()
  {
    Console.WriteLine("Hacked!");
    base.Method2();
  }
  public void Method4()
  {
    Console.WriteLine("Hacked!");
  }
}