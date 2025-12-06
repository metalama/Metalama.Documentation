namespace Doc.IntroduceParameter;
internal class Foo
{
  [RegisterInstance]
  public Foo(IInstanceRegistry? instanceRegistry = default)
  {
    instanceRegistry.Register(this);
  }
}
internal class Bar : Foo
{
  public Bar(IInstanceRegistry instanceRegistry = null) : base(instanceRegistry)
  {
  }
}