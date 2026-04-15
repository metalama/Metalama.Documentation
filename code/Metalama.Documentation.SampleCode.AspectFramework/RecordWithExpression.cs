// This is public domain Metalama sample code.

using System;

namespace Doc.RecordWithExpression;

[ValidateAfterInitialization]
public sealed partial record Product( string Name, decimal Price );

internal class Program
{
    private static void Main()
    {
        var product = new Product( "Widget", 9.99m );
        Console.WriteLine( $"Created: {product}" );

        var discounted = product with { Price = 7.99m };
        Console.WriteLine( $"Discounted: {discounted}" );
    }
}
