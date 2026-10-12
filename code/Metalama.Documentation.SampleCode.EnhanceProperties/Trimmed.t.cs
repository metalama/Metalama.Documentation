using System;
namespace Doc.Trimmed;
public class Details
{
  private string? _code;
  [Trim]
  public string? Code
  {
    get
    {
      return _code;
    }
    set
    {
      _code = value?.Trim();
    }
  }
}
public class Program
{
  public static void Main()
  {
    Details detail1 = new()
    {
      Code = "   GW12345  "
    };
    Console.WriteLine($"Code='{detail1.Code}'");
  }
}