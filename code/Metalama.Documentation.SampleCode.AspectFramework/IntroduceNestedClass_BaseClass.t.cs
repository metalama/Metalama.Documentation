namespace Doc.IntroduceNestedClass_BaseClass;
[Builder]
internal class Material
{
  public string Name { get; }
  public double Density { get; }
  class Builder : BaseFactory
  {
  }
}
public abstract class BaseFactory
{
}