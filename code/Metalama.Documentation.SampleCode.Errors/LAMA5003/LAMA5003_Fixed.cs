// This is public domain Metalama sample code.

namespace Doc.LAMA5003.Fixed;

public class GreetingService
{
    // Fixed: no explicit [NotNull] contract; the fabric adds it.
    public string Greet( string name ) => $"Hello, {name}.";
}
