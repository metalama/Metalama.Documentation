using System;
using Metalama.Framework.RunTime;
namespace Doc.FactoryFrameworkLazy;
// A class using the lazy logging aspect.
// The factory framework will lazily call Create() on first property access.
public partial class LazyGreeter
{
  [LogLazy]
  public void SayHello()
  {
    _logger.Log("Entering LazyGreeter.SayHello()");
    try
    {
      Console.WriteLine("Hello!");
      return;
    }
    finally
    {
      _logger.Log("Leaving LazyGreeter.SayHello()");
    }
  }
  private ILogger? _loggerCache;
  private Func<IServiceFactory<ILogger>> _loggerFunc;
  public LazyGreeter([AspectGenerated] Func<IServiceFactory<ILogger>>? logger = null)
  {
    this._loggerFunc = logger ?? throw new System.ArgumentNullException(nameof(logger));
  }
  private ILogger _logger
  {
    get
    {
      return _loggerCache ??= _loggerFunc.Invoke().Create();
    }
  }
}