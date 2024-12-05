using System;
namespace Doc.RegisterInstance;
[RegisterInstance]
internal class DemoClass
{
  public DemoClass()
  {
    _instanceRegistryHandle = InstanceRegistry.Register(this);
  }
  public DemoClass(int i) : this()
  {
  }
  public DemoClass(string s)
  {
    _instanceRegistryHandle = InstanceRegistry.Register(this);
  }
  private IDisposable _instanceRegistryHandle;
}