// This is public domain Metalama sample code.

using System;

namespace Doc.AfterObjectInitializer;

public record EntityInitialized( string TypeName, object Entity );

public static class DomainEvents
{
    public static event Action<EntityInitialized>? Published;

    public static void Publish( EntityInitialized e ) => Published?.Invoke( e );
}
