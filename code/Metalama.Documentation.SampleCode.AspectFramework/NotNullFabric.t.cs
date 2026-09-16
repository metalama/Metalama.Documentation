using System;
namespace Doc.NotNullFabric;
public class PublicType
{
  public void PublicMethod(string notNullableString, string? nullableString, int? nullableInt)
  {
    if (notNullableString == null)
    {
      throw new ArgumentNullException("notNullableString");
    }
  }
}