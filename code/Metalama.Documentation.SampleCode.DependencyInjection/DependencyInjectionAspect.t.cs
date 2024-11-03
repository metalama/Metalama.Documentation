using System;
using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace Doc.DependencyInjectionAspect;
public class DependencyInjectionAspect
{
  // This dependency will be optional because the field is nullable.
  [Dependency]
  private ILogger? _logger;
  // This dependency will be required because the field is non-nullable.
  [Dependency]
  private IHostEnvironment _environment;
  [Dependency(IsLazy = true)]
  private IHostApplicationLifetime _lifetime
  {
    get
    {
      return _lifetimeCache ??= _lifetimeFunc.Invoke();
    }
    set
    {
      throw new NotSupportedException("Cannot set '_lifetime' because of the dependency aspect.");
    }
  }
  public void DoWork()
  {
    this._logger?.LogDebug("Doing some work.");
    if (!this._environment.IsProduction())
    {
      this._lifetime.StopApplication();
    }
  }
  private IHostApplicationLifetime? _lifetimeCache;
  private Func<IHostApplicationLifetime> _lifetimeFunc;
  public DependencyInjectionAspect(ILogger<DependencyInjectionAspect> logger = null, IHostEnvironment? environment = null, Func<IHostApplicationLifetime>? lifetime = null)
  {
    this._logger = logger;
    this._environment = environment ?? throw new System.ArgumentNullException(nameof(environment));
    this._lifetimeFunc = lifetime ?? throw new System.ArgumentNullException(nameof(lifetime));
  }
}