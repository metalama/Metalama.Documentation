// This is public domain Metalama sample code.

namespace Doc.AfterLastInstanceConstructor;

[NotifyConstructed]
public partial class Connection
{
    public Connection() { }

    public Connection( string connectionString ) { }

    public Connection( string host, int port ) : this( $"{host}:{port}" ) { }
}

public partial class SecureConnection : Connection
{
    public SecureConnection( string connectionString, string certificate )
        : base( connectionString ) { }
}
