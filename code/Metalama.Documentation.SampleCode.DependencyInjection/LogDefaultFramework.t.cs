using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Framework.RunTime;
using System;
namespace Doc.LogDefaultFramework;
// The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
public class Worker : IConsoleMain
{
  [Log]
  public void Execute()
  {
    try
    {
      _messageWriter.Write("Worker.Execute() started.");
      Console.WriteLine("Hello, world.");
      return;
    }
    finally
    {
      _messageWriter.Write("Worker.Execute() completed.");
    }
  }
  private IMessageWriter _messageWriter;
  public Worker([AspectGenerated] IMessageWriter? messageWriter = null)
  {
    this._messageWriter = messageWriter ?? throw new System.ArgumentNullException(nameof(messageWriter));
  }
}