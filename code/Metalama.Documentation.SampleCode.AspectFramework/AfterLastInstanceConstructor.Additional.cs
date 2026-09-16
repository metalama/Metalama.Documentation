// This is public domain Metalama sample code.

using System;

namespace Doc.AfterLastInstanceConstructor;

public record EntityCreated( string TypeName, object Entity );

public static class DomainEvents
{
    public static event Action<EntityCreated>? Published;

    public static void Publish( EntityCreated e ) => Published?.Invoke( e );
}
