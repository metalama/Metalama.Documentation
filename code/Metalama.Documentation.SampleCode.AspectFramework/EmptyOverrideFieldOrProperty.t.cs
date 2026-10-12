namespace Doc;
internal class EmptyOverrideFieldOrPropertyExample
{
  private int _field;
  [EmptyOverrideFieldOrProperty]
  public int Field
  {
    get
    {
      return _field;
    }
    set
    {
      _field = value;
    }
  }
  private string? _property;
  [EmptyOverrideFieldOrProperty]
  public string? Property
  {
    get
    {
      return _property;
    }
    set
    {
      _property = value;
    }
  }
}