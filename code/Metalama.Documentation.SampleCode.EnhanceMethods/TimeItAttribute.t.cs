using System;
using System.Diagnostics;
using System.Threading;
namespace Doc.TimeIt
{
  public class TimeItDemo
  {
    [TimeIt]
    public static int SimulatedDelay1()
    {
      Console.WriteLine("Started executing TimeItDemo.SimulatedDelay1()");
      Stopwatch sw = new Stopwatch();
      sw.Start();
      int result;
      //Simulating a random delay between 500 ms to 2 secs
      Thread.Sleep(new Random().Next(500, 2000));
      result = 0;
      sw.Stop();
      Console.WriteLine("Finished executing TimeItDemo.SimulatedDelay1()");
      Console.WriteLine($"Time taken :{sw.ElapsedMilliseconds} ms");
      return result;
    }
    [TimeIt]
    public static int SimulatedDelay2()
    {
      Console.WriteLine("Started executing TimeItDemo.SimulatedDelay2()");
      Stopwatch sw = new Stopwatch();
      sw.Start();
      int result;
      //Simulating a random delay of 3 secs
      Thread.Sleep(3000);
      result = 0;
      sw.Stop();
      Console.WriteLine("Finished executing TimeItDemo.SimulatedDelay2()");
      Console.WriteLine($"Time taken :{sw.ElapsedMilliseconds} ms");
      return result;
    }
    public static void Main()
    {
      SimulatedDelay1();
      SimulatedDelay2();
    }
  }
}