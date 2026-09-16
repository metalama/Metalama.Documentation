// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;

#if METALAMA
using Microsoft.Extensions.Logging;
#endif

namespace Doc.LogCustomFramework;

// The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
public class ConsoleMain : IConsoleMain
{
    [Log]
    public void Execute()
    {
#if METALAMA
            _logger.LogInformation( "Hello, world." );
#endif
    }
}