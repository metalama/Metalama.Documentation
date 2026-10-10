using System;
using System.Collections.Generic;
namespace Doc.LAMA0203.Fixed;
[PropertyTypes]
public partial class Customer
{
  public string? Name { get; set; }
  public int Age { get; set; }
  public Dictionary<string, string> GetPropertyTypes()
  {
    return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
      {
        "Name",
        "string?"
      },
      {
        "Age",
        "int"
      }
    };
  }
}