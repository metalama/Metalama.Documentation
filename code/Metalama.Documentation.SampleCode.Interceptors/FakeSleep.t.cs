using System.Threading;
namespace Doc.FakeSleep;
public static class Backoff
{
  public static void WaitBeforeAttempt(int attempt) => VirtualClock.Sleep(1000 * attempt);
}