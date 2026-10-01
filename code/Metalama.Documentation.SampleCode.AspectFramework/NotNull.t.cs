using System;
namespace Doc.NotNull;
internal class Foo
{
  public void Method1([NotNull] string s)
  {
    if (s == null)
    {
      throw new ArgumentNullException(nameof(s));
    }
  }
  public void Method2([NotNull] out string s)
  {
    s = null !;
    if (s == null)
    {
      throw new PostConditionFailedException($"'{nameof(s)}' cannot be null when the method returns.");
    }
  }
  [return: NotNull]
  public string Method3()
  {
    string returnValue;
    returnValue = null !;
    if (returnValue == null)
    {
      throw new PostConditionFailedException($"'{nameof(returnValue)}' cannot be null when the method returns.");
    }
    return returnValue;
  }
  private string _property = default !;
  [NotNull]
  public string Property
  {
    get
    {
      return _property;
    }
    set
    {
      if (value == null)
      {
        throw new ArgumentNullException(nameof(value));
      }
      _property = value;
    }
  }
}
public class PostConditionFailedException : Exception
{
  public PostConditionFailedException(string message) : base(message)
  {
  }
}