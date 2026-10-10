// Warning LAMA5003 on `name`: `The [NotNullAttribute] contract is redundant because the [NotNull] contract is automatically added by a fabric.`
using System;
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5003.Error;
public class GreetingService
{
  // Warning: the fabric already adds [NotNull] to this parameter.
  public string Greet([NotNull] string name)
  {
    if (name == null !)
    {
      throw new ArgumentNullException("name", "The 'name' parameter must not be null.");
    }
    return $"Hello, {name}.";
  }
}