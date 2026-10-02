using Metalama.Patterns.Contracts;
using System;
namespace Doc.EnumDataTypeContract;
public class Message
{
  private string? _color;
  [EnumDataType(typeof(ConsoleColor))]
  public string? Color
  {
    get
    {
      return _color;
    }
    set
    {
      if (value != null ! && !EnumDataTypeAttributeHelper.IsValidEnumValue(value, typeof(ConsoleColor)))
      {
        throw new ArgumentException("The 'Color' property must be a valid string?.", "value");
      }
      _color = value;
    }
  }
}