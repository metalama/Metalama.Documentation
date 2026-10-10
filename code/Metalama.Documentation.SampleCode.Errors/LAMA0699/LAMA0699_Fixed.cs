// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0699.Fixed;

[LogConstructorErrors]
public class DatabaseConnection
{
    public DatabaseConnection( string connectionString )
    {
        Console.WriteLine( $"Connecting to '{connectionString}'." );
    }
}
