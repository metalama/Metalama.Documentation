using System;
namespace Doc.RaiseEvent;
public class MyService
{
  public event EventHandler? StatusChanged;
  [RaiseStatusChanged]
  public void UpdateStatus()
  {
    // Do some work.
    Console.WriteLine("Updating status.");
    object result = null;
    StatusChanged?.Invoke((object? )this, EventArgs.Empty);
  }
}