// This is public domain Metalama sample code.

using System;

namespace Doc.MixedInitialization;

[TrackInitialization]
public sealed partial class Customer
{
    public Customer( int id )
    {
        this.Id = id;
        Console.WriteLine( $"  Constructor: Id = {this.Id}" );
    }

    public int Id { get; }

    public string Name { get; init; } = "";

    public string Email { get; init; } = "";
}

internal class Program
{
    private static void Main()
    {
        Console.WriteLine( "Creating customer:" );
        var customer = new Customer( 1 ) { Name = "Alice", Email = "alice@example.com" };
        Console.WriteLine( $"  Result: {customer.Name} ({customer.Email})" );
    }
}
