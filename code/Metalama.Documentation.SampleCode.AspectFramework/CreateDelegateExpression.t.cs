using System;
namespace Doc.CreateDelegateExpression;
public class TestClass
{
  [RegisterCallback(nameof(OnCompleted))]
  public void DoWork()
  {
    Console.WriteLine("Doing work...");
    object result = null;
    Action<string> callback = OnCompleted;
    callback.Invoke("Operation completed.");
  }
  private void OnCompleted(string message)
  {
    Console.WriteLine($"Callback: {message}");
  }
}