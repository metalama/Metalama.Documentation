using System;
using Metalama.Patterns.Contracts;
namespace Doc.DisablePreconditions;
// Contracts are enabled for this class (default behavior).
public class EnabledService
{
  public void Process([NotNull] string input, [Range(0, 100)] int percent)
  {
    if (input == null!)
    {
      throw new ArgumentNullException("input", "The 'input' parameter must not be null.");
    }
    if (percent is < 0 or > 100)
    {
      throw new ArgumentOutOfRangeException("percent", percent, "The 'percent' parameter must be in the range [0, 100].");
    }
  }
}
// Contracts are disabled for this class via the fabric.
public class DisabledService
{
  public void Process([NotNull] string input, [Range(0, 100)] int percent)
  {
  }
}
// Contracts are selectively disabled for specific methods.
public class MixedService
{
  // Contracts enabled (default).
  public void ValidatedMethod([NotNull] string input)
  {
    if (input == null!)
    {
      throw new ArgumentNullException("input", "The 'input' parameter must not be null.");
    }
  }
  // Contracts disabled for this method via the fabric.
  public void UnvalidatedMethod([NotNull] string input)
  {
  }
}
