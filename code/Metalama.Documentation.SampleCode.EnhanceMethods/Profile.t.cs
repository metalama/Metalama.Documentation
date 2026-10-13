using System;
using System.Diagnostics;
using System.Threading;
namespace Doc.Profile;
public class Program
{
  [Profile]
  public static int SimulatedDelay()
  {
    var sw = Stopwatch.StartNew();
    try
    {
      // Simulating a random delay between 500 ms to 2 secs
      Thread.Sleep(new Random().Next(500, 2000));
      return 0;
    }
    finally
    {
      Console.WriteLine($"Program.SimulatedDelay() executed in {sw.ElapsedMilliseconds} ms.");
    }
  }
  public static void Main()
  {
    SimulatedDelay();
  }
}