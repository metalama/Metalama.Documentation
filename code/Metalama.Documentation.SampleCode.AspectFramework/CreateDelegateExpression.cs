// This is public domain Metalama sample code.

using System;

namespace Doc.CreateDelegateExpression;

public static class AppEvents
{
    public static event Action? Shutdown;
}

[AutoConnect]
public partial class MyService
{
    private void OnShutdown()
    {
        Console.WriteLine( "Cleaning up..." );
    }
}
