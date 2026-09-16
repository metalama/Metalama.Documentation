namespace Doc.IntroduceNestedClass_Members;
[Builder]
internal class Material
{
  public string Name { get; }
  public double Density { get; }
  class Builder
  {
    private double Density { get; set; }
    private string Name { get; set; }
  }
}