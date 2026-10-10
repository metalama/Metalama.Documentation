using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5001.Fixed;
public class ServerSettings
{
  private ushort _port;
  // Fixed: a ushort can hold the values of the range.
  [Range(1024, 65535)]
  public ushort Port
  {
    get
    {
      return _port;
    }
    set
    {
      if (value < 1024)
      {
        throw new ArgumentOutOfRangeException("value", value, "The 'Port' property must be in the range [1024, 65535].");
      }
      _port = value;
    }
  }
}