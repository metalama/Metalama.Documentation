using System;
namespace Doc.Trim;
internal class Foo
{
  public void Method1([Trim] string nonNullableString, [Trim] string? nullableString)
  {
    nonNullableString = nonNullableString.Trim();
    nullableString = nullableString?.Trim();
    Console.WriteLine($"nonNullableString='{nonNullableString}', nullableString='{nullableString}'");
  }
  public string Property { get; set; }
}
internal class Program
{
  public static void Main()
  {
    var foo = new Foo();
    foo.Method1("     A  ", "   B ");
    foo.Property = "    C   ";
    Console.WriteLine($"Property='{foo.Property}'");
  }
}