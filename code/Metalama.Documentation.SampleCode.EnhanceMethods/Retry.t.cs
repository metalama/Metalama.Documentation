using System;
using System.Net;
using System.Threading;
namespace Doc.RetryFew;
public class Exchange
{
  [Retry]
  public double GetExchangeRate()
  {
    for (var i = 0;; i++)
    {
      try
      {
        // Simulates a call to an exchange rate web API.
        // Sometimes the connection may fail.
        var n = new Random(5).Next(20);
        if (n % 2 == 0)
        {
          return 0.5345;
        }
        else
        {
          throw new WebException("The service is not available.");
        }
      }
      catch (Exception e)when (i < 3)
      {
        Console.WriteLine($"{e.Message}. Retrying in 100 ms.");
        Thread.Sleep(100);
      }
    }
  }
}
public class Program
{
  public static void Main()
  {
    var x = new Exchange();
    var rate = x.GetExchangeRate();
    Console.WriteLine(rate);
  }
}