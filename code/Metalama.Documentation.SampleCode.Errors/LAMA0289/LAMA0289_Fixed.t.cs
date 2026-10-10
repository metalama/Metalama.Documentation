using System;
namespace Doc.LAMA0289.Fixed;
internal class SessionService
{
  [LogToken]
  public void Open(string token)
  {
    var normalizedToken = token?.ToLower();
    Console.WriteLine($"Normalized token: {normalizedToken}");
    Console.WriteLine("Session opened.");
  }
}