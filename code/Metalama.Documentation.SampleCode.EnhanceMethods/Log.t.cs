using System;
namespace Doc.Logging;
public class Program
{
  [Log]
  public static void SayHello(string name)
  {
    try
    {
      Console.WriteLine("Started Program.SayHello(string)");
      Console.WriteLine($"Hello {name}");
      return;
    }
    finally
    {
      Console.WriteLine("Finished Program.SayHello(string)");
    }
  }
  public static void Main()
  {
    SayHello("Your Majesty");
  }
}