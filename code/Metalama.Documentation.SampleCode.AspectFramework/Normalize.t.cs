namespace Doc.Normalize;
internal class Foo
{
  private string? _property;
  [Normalize]
  public string? Property
  {
    get
    {
      return _property;
    }
    set
    {
      _property = value?.Trim().ToLowerInvariant();
    }
  }
}