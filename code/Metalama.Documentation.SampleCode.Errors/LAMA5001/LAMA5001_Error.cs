// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5001.Error;

public class ServerSettings
{
    // Error: a byte can't hold any value between 1024 and 65535.
    [Range( 1024, 65535 )]
    public byte Port { get; set; }
}
