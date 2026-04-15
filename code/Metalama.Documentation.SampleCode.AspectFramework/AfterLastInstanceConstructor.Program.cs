// This is public domain Metalama sample code.

using System;

namespace Doc.AfterLastInstanceConstructor;

internal class Program
{
    private static void Main()
    {
        DomainEvents.Published +=
            e => Console.WriteLine( $"Published: {e.TypeName}." );

        _ = new Order( "alice" );
        _ = new RecurringOrder( "bob", TimeSpan.FromDays( 30 ) );
    }
}
