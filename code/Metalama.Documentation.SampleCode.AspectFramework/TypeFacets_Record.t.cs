namespace Doc.TypeFacets_Record;
[ToTuple]
public partial record Product(string Code, string Name, decimal Price)
{
  public string? Description { get; init; }
  public (string Code, string Name, decimal Price) ToTuple()
  {
    return (Code, Name, Price);
  }
}