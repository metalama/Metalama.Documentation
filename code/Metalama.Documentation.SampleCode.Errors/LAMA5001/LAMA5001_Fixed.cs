// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5001.Fixed;

public class ServerSettings
{
    // Fixed: a ushort can hold the values of the range.
    [Range( 1024, 65535 )]
    public ushort Port { get; set; }
}
