// This is public domain Metalama sample code.

using System;

namespace Doc.AfterObjectInitializer;

internal class Program
{
    private static void Main()
    {
        DomainEvents.Published +=
            e => Console.WriteLine( $"Published: {e.TypeName}." );

        var doc = new Document { Id = "doc-1", Title = "Spec" };
        _ = new Report { Id = "r-1", Date = new DateOnly( 2026, 4, 15 ) };
        _ = doc with { Id = "doc-2" };
    }
}
