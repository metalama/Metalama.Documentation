using System;
namespace Doc.LAMA5003.Fixed;
public class GreetingService
{
  // Fixed: no explicit [NotNull] contract; the fabric adds it.
  public string Greet(string name)
  {
    if (name == null !)
    {
      throw new ArgumentNullException("name", "The 'name' parameter must not be null.");
    }
    return $"Hello, {name}.";
  }
}