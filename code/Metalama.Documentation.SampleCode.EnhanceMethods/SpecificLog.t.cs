using System;
namespace Doc.SpecificLog
{
  public class SpecificLogDemo
  {
    [SpecificLog]
    public static void SayHello(string name)
    {
      try
      {
        Console.WriteLine("Started executing SpecificLogDemo.SayHello(string)");
        System.Console.WriteLine($"Hello {name}");
        return;
      }
      finally
      {
        Console.WriteLine("Finished executing SpecificLogDemo.SayHello(string)");
      }
    }
    public static void Main()
    {
      SayHello("Gael");
    }
  }
}