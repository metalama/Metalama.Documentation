// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.ImplementInterfaceFourWays;

public class GenerateResetAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Implement IDisposable.
        builder.ImplementInterface( typeof(IDisposable), OverrideStrategy.Ignore );
    }

    // Implements IDisposable.Dispose.
    [InterfaceMember]
    public void Dispose()
    {
        Console.WriteLine( "Disposing..." );
    }

     // Introduce a Reset method demonstrating four ways to call Dispose.
    [Introduce]
    public void Reset()
    {
        // Option 1: Access the aspect template member directly.
        meta.InsertComment( "Option 1: this.Dispose()" );
        this.Dispose();

        // Option 2: Use meta.This for dynamic code.
        meta.InsertComment( "Option 2: meta.This.Dispose()" );
        meta.This.Dispose();

        // Option 3: Use the invoker API on the interface method.
        meta.InsertComment( "Option 3: disposeMethod.Invoke()" );
        var disposableInterface = (INamedType) TypeFactory.GetType( typeof(IDisposable) );
        var disposeMethod = disposableInterface.Methods.Single( m => m.Name == "Dispose" );
        disposeMethod.Invoke();

        // Option 4: Cast to interface (useful for explicit implementations).
        meta.InsertComment( "Option 4: ((IDisposable)meta.This).Dispose()" );
        ((IDisposable) meta.This).Dispose();
    }
}
