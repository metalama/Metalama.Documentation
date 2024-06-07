using Metalama.Documentation.Helpers.ConsoleApp;
using Microsoft.Extensions.Logging;
namespace Doc.LogCustomFramework;
// The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
public class ConsoleMain : IConsoleMain
{
  [Log]
  public void Execute()
  {
    try
    {
      _logger.LogWarning("ConsoleMain.Execute() started.");
      _logger.LogInformation("Hello, world.");
      return;
    }
    finally
    {
      _logger.LogWarning("ConsoleMain.Execute() completed.");
    }
  }
  private ILogger _logger;
  public ConsoleMain(ILogger<ConsoleMain> logger = default)
  {
    this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
  }
}