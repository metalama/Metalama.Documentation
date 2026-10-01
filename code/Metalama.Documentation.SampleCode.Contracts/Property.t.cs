using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;
namespace Doc.Contracts.Property;
public interface IItem
{
  [NotEmpty]
  string Key { get; }
  [NotEmpty(Direction = ContractDirection.Both)]
  string Value { get; set; }
}
public class Item : IItem
{
  private readonly string _key = default !;
  public string Key
  {
    get
    {
      var returnValue = _key;
      if (returnValue.Length <= 0)
      {
        throw new PostconditionViolationException("The 'Key' property must not be null or empty.");
      }
      return returnValue;
    }
    private init
    {
      _key = value;
    }
  }
  private string _value = default !;
  public string Value
  {
    get
    {
      var returnValue = _value;
      if (returnValue.Length <= 0)
      {
        throw new PostconditionViolationException("The 'Value' property must not be null or empty.");
      }
      return returnValue;
    }
    set
    {
      if (value.Length <= 0)
      {
        throw new PostconditionViolationException("The 'Value' property must not be null or empty.");
      }
      _value = value;
    }
  }
  public Item(string key, string value)
  {
    this.Key = key;
    this.Value = value;
  }
}