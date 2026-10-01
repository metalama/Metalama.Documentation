using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
namespace Doc.AdvisingTypeFabric;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class MyClass
{
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private class Fabric : TypeFabric
  {
    [Template]
    public int MethodTemplate([CompileTime] int index) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  public int Method0()
  {
    return 0;
  }
  public int Method1()
  {
    return 1;
  }
  public int Method2()
  {
    return 2;
  }
  public int Method3()
  {
    return 3;
  }
  public int Method4()
  {
    return 4;
  }
  public int Method5()
  {
    return 5;
  }
  public int Method6()
  {
    return 6;
  }
  public int Method7()
  {
    return 7;
  }
  public int Method8()
  {
    return 8;
  }
  public int Method9()
  {
    return 9;
  }
}