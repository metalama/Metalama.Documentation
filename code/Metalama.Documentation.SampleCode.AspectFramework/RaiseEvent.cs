// This is public domain Metalama sample code.

using System;

namespace Doc.RaiseEvent;

public class MyService
{
    public event EventHandler? StatusChanged;

    [RaiseStatusChanged]
    public void UpdateStatus()
    {
        // Do some work.
        Console.WriteLine( "Updating status." );
    }
}
