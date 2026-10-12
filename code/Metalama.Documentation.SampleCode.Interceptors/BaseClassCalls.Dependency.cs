// This is public domain Metalama sample code.

using System;

namespace Doc.BaseClassCalls;

// A base class of a UI framework, compiled in another project, like Control in Windows Forms.
public class Control
{
    // A protected method: only derived classes can call it.
    protected void UpdateStyles() => Console.WriteLine( $"{this.GetType().Name} styles updated." );
}
