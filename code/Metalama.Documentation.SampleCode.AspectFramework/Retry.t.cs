using System;
using System.Threading;
namespace Doc.Retry;
internal class Foo
{
  [Retry]
  private void RetryDefault()
  {
    for (var i = 0;; i++)
    {
      try
      {
        throw new InvalidOperationException();
        return;
      }
      catch (Exception e)when (i < 5)
      {
        Console.WriteLine($"{e.Message}. Retrying in 100 ms.");
        Thread.Sleep(100);
      }
    }
  }
  [Retry(MaxAttempts = 10)]
  private void RetryTenTimes()
  {
    for (var i = 0;; i++)
    {
      try
      {
        throw new InvalidOperationException();
        return;
      }
      catch (Exception e)when (i < 10)
      {
        Console.WriteLine($"{e.Message}. Retrying in 100 ms.");
        Thread.Sleep(100);
      }
    }
  }
}