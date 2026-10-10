// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0528.Error;

public class DisposableAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // If the type already has a Dispose method, override it.
        builder.ImplementInterface( typeof(IDisposable), whenExists: OverrideStrategy.Override );
    }

    [InterfaceMember]
    public void Dispose()
    {
        meta.Proceed();
        Console.WriteLine( $"{meta.Target.Type.Name} disposed." );
    }
}
