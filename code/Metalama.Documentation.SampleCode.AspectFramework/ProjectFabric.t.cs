using System;
namespace Doc.ProjectFabric_;
internal class Class1
{
  public void Method1()
  {
    Console.WriteLine("Executing Class1.Method1().");
    try
    {
      Console.WriteLine("Inside Class1.Method1");
      return;
    }
    finally
    {
      Console.WriteLine("Exiting Class1.Method1().");
    }
  }
  public void Method2()
  {
    Console.WriteLine("Executing Class1.Method2().");
    try
    {
      Console.WriteLine("Inside Class1.Method2");
      return;
    }
    finally
    {
      Console.WriteLine("Exiting Class1.Method2().");
    }
  }
}
internal class Class2
{
  public void Method1()
  {
    Console.WriteLine("Executing Class2.Method1().");
    try
    {
      Console.WriteLine("Inside Class2.Method1");
      return;
    }
    finally
    {
      Console.WriteLine("Exiting Class2.Method1().");
    }
  }
  public void Method2()
  {
    Console.WriteLine("Executing Class2.Method2().");
    try
    {
      Console.WriteLine("Inside Class2.Method2");
      return;
    }
    finally
    {
      Console.WriteLine("Exiting Class2.Method2().");
    }
  }
}