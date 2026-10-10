namespace Doc.LAMA0508.Fixed;
// Fixed: the [Describe] aspect introduces a static method that is not sealed.
[Describe]
public partial class Product
{
  public string? Name { get; set; }
  public static string GetTypeDescription()
  {
    return "Product";
  }
}