using System;
namespace Doc.SimpleNotNull;
public class TheClass
{
  private string _field = "Field";
  [NotNull]
  public string Field
  {
    get
    {
      return _field;
    }
    set
    {
      if (value == null !)
      {
        throw new ArgumentNullException(nameof(value));
      }
      _field = value;
    }
  }
  private string _property = "Property";
  [NotNull]
  public string Property
  {
    get
    {
      return _property;
    }
    set
    {
      if (value == null !)
      {
        throw new ArgumentNullException(nameof(value));
      }
      _property = value;
    }
  }
  public void Method([NotNull] string parameter)
  {
    if (parameter == null !)
    {
      throw new ArgumentNullException(nameof(parameter));
    }
  }
}