using System;
namespace Doc.ImportAttributeFramework;
// A class using the aspect.
public partial class Greeter
{
  [Log]
  public void SayHello()
  {
    _logger.Log("Entering Greeter.SayHello()");
    try
    {
      Console.WriteLine("Hello!");
      return;
    }
    finally
    {
      _logger.Log("Leaving Greeter.SayHello()");
    }
  }
  [Import]
  public ILogger _logger { get; set; }
}