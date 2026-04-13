namespace Doc.AdvisingTypeFabricDiagnostics;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public partial class MyClass
{
  public string? Name { get; set; }
  public override string ToString()
  {
    return "MyClass (advised by fabric)";
  }
}
