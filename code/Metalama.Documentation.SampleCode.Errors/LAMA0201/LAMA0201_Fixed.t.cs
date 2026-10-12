using System.Collections.Generic;
namespace Doc.LAMA0201.Fixed;
[Tags]
public partial class Customer
{
  public string? Name { get; set; }
  public object GetTags()
  {
    return new List<object>
    {
      "Customer",
      "Doc.LAMA0201.Fixed.Customer"
    };
  }
}