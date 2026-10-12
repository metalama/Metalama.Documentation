using System;
using System.Diagnostics;
namespace Doc.ProjectFabric_TwoAspects;
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
public class Class2
{
  public void Method1()
  {
    Console.WriteLine("Executing Class2.Method1().");
    try
    {
      var stopwatch = Stopwatch.StartNew();
      try
      {
        Console.WriteLine("Inside Class2.Method1");
      }
      finally
      {
        Console.WriteLine($"Class2.Method1() completed in {stopwatch.ElapsedMilliseconds}.");
      }
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
      var stopwatch = Stopwatch.StartNew();
      try
      {
        Console.WriteLine("Inside Class2.Method2");
      }
      finally
      {
        Console.WriteLine($"Class2.Method2() completed in {stopwatch.ElapsedMilliseconds}.");
      }
      return;
    }
    finally
    {
      Console.WriteLine("Exiting Class2.Method2().");
    }
  }
}