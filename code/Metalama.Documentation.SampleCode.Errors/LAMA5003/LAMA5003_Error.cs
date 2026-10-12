// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.LAMA5003.Error;

public class GreetingService
{
    // Warning: the fabric already adds [NotNull] to this parameter.
    public string Greet( [NotNull] string name ) => $"Hello, {name}.";
}
