// This is public domain Metalama sample code.

using System;

namespace Doc.BaseClassCalls;

// A base class of a UI framework, compiled in another project, like Control in Windows Forms.
public class Control
{
    public void Invalidate() => Console.WriteLine( $"{this.GetType().Name} invalidated." );

    public void Focus() => Console.WriteLine( $"{this.GetType().Name} focused." );
}
