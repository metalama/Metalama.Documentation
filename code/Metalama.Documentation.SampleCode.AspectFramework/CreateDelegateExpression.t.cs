using System;
namespace Doc.CreateDelegateExpression;
public static class AppEvents
{
  public static event Action? Shutdown;
}
[AutoConnect]
public partial class MyService : IDisposable
{
  private void OnShutdown()
  {
    Console.WriteLine("Cleaning up...");
  }
  public MyService()
  {
    AppEvents.Shutdown += OnShutdown;
  }
  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize(this);
  }
  protected virtual void Dispose(bool disposing)
  {
    if (disposing)
    {
      AppEvents.Shutdown -= OnShutdown;
    }
  }
}