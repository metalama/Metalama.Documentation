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
  private ILogger _logger1 = default !;
  [Import]
  public ILogger _logger
  {
    get
    {
      return _logger1;
    }
    set
    {
      _logger1 = value;
    }
  }
}
