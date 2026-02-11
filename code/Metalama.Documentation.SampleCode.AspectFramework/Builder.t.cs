using System.ComponentModel.DataAnnotations;
namespace Doc.Builder_;
[Builder]
internal class Material
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
    private Builder(string Name)
    {
      this.Name = Name;
    }
    private double Density { get; set; }
    private string Name { get; set; }
    public Material Build(string Name, double Density)
    {
      return new Material(this.Name, this.Density);
    }
  }
}