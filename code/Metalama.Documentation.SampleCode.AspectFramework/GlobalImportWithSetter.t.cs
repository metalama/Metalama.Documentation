using System;
using System.Collections.Generic;
namespace Doc.GlobalImportWithSetter;
internal class Foo
{
  private IFormatProvider? _formatProvider1;
  [Import]
  private IFormatProvider? _formatProvider
  {
    get
    {
      var service = _formatProvider1;
      if (service == null)
      {
        service = (IFormatProvider? )ServiceLocator.ServiceProvider.GetService(typeof(IFormatProvider));
        _formatProvider1 = service;
      }
      return service;
    }
    set
    {
      throw new NotSupportedException();
    }
  }
}
internal class ServiceLocator : IServiceProvider
{
  private static readonly ServiceLocator _instance = new();
  private readonly Dictionary<Type, object> _services = new();
  public static IServiceProvider ServiceProvider => _instance;
  object? IServiceProvider.GetService(Type serviceType)
  {
    this._services.TryGetValue(serviceType, out var value);
    return value;
  }
  public static void AddService<T>(T service)
    where T : class => _instance._services[typeof(T)] = service;
}