using System;
using Metalama.Framework.RunTime;
namespace Doc.FactoryFramework;
// A class using the logging aspect.
// The factory framework will pull IServiceFactory<ILogger> and call Create().
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
  private ILogger _logger;
  public Greeter([AspectGenerated] IServiceFactory<ILogger> logger = null)
  {
    this._logger = logger.Create();
  }
}
