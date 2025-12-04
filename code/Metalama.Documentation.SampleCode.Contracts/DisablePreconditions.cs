// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.DisablePreconditions;

// Contracts are enabled for this class (default behavior).
public class EnabledService
{
    public void Process( [NotNull] string input, [Range( 0, 100 )] int percent )
    {
    }
}

// Contracts are disabled for this class via the fabric.
public class DisabledService
{
    public void Process( [NotNull] string input, [Range( 0, 100 )] int percent )
    {
    }
}

// Contracts are selectively disabled for specific methods.
public class MixedService
{
    // Contracts enabled (default).
    public void ValidatedMethod( [NotNull] string input )
    {
    }

    // Contracts disabled for this method via the fabric.
    public void UnvalidatedMethod( [NotNull] string input )
    {
    }
}
