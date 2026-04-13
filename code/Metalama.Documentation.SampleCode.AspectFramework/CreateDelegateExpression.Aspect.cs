// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.CreateDelegateExpression;

public class AutoConnectAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Implement IDisposable.
        builder.ImplementInterface( typeof(IDisposable) );

        // Introduce or override Dispose(bool).
        builder.IntroduceMethod(
            nameof(this.DisposeImpl),
            whenExists: OverrideStrategy.Override,
            buildMethod: m => m.Name = "Dispose" );

        // Override all constructors.
        foreach ( var constructor in builder.Target.Constructors )
        {
            builder.With( constructor ).Override( nameof(this.OverrideConstructor) );
        }
    }

    [Template]
    private void OverrideConstructor()
    {
        meta.Proceed();

        // Register the OnShutdown handler.
        var onShutdown = meta.Target.Type.Methods.OfName( "OnShutdown" ).Single();
        AppEvents.Shutdown += onShutdown.CreateDelegateExpression().Value!;
    }

    [InterfaceMember]
    public void Dispose()
    {
        meta.This.Dispose( true );
        GC.SuppressFinalize( meta.This );
    }

    [Template]
    protected virtual void DisposeImpl( bool disposing )
    {
        meta.Proceed();

        if ( disposing )
        {
            // Unregister the OnShutdown handler.
            var onShutdown = meta.Target.Type.Methods.OfName( "OnShutdown" ).Single();
            AppEvents.Shutdown -= onShutdown.CreateDelegateExpression().Value!;
        }
    }
}
