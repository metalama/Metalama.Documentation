using System;
using System.Threading.Tasks;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
namespace Doc.LogServiceLocator;
// The class using the Log aspect. This class is NOT instantiated by any dependency injection container.
public class Worker
{
  [Log]
  public Task ExecuteAsync()
  {
    try
    {
      _messageWriter.Write("Worker.ExecuteAsync() started.");
      Console.WriteLine("Hello, world.");
      return Task.CompletedTask;
    }
    finally
    {
      _messageWriter.Write("Worker.ExecuteAsync() completed.");
    }
  }
  private IMessageWriter _messageWriter;
  public Worker()
  {
    _messageWriter = (IMessageWriter)ServiceProviderProvider.ServiceProvider().GetService(typeof(IMessageWriter)) ?? throw new InvalidOperationException("The service 'IMessageWriter' could not be obtained from the service locator.");
  }
}