using System.ComponentModel.DataAnnotations;
namespace Doc.Builder_;
[Builder]
internal partial class Material
{
  [Required]
  public string Name { get; }
  public double Density { get; }
  private Material(string Name, double Density)
  {
    this.Name = Name;
    this.Density = Density;
  }
  public class Builder
  {
    public Builder(string Name)
    {
      this.Name = Name;
    }
    public double Density { get; set; }
    public string Name { get; set; }
    public Material Build()
    {
      return new Material(Name, Density);
    }
  }
}
internal static class Program
{
  public static void Main()
  {
    var material = new Material.Builder("Steel")
    {
      Density = 7.87
    }.Build();
  }
}