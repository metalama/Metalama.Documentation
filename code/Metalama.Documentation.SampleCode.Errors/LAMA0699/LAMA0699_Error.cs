// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0699.Error;

[LogConstructorErrors]
public class DatabaseConnection
{
    public DatabaseConnection( string connectionString )
    {
        Console.WriteLine( $"Connecting to '{connectionString}'." );
    }
}
