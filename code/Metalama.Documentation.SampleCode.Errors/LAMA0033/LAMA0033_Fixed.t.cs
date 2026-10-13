namespace Doc.LAMA0033.Fixed;
[Describe]
internal partial class Calculator
{
  public int Add(int a, int b) => a + b;
  public string Describe()
  {
    return "This object is enhanced by the [Describe] aspect.";
  }
}