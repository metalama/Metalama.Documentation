// Error CS0234 on `LogCustomFramework`: `The type or namespace name 'LogCustomFramework' does not exist in the namespace 'Doc' (are you missing an assembly reference?)`
// Error CS0246 on `ConsoleMain`: `The type or namespace name 'ConsoleMain' could not be found (are you missing a using directive or an assembly reference?)`
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