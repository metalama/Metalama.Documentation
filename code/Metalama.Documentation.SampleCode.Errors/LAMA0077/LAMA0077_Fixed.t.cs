namespace Doc.LAMA0077.Fixed;
// The duplicate weaver is removed: the weaver comes only from the referenced project.
[Virtualize]
internal class Calculator
{
  public virtual int Add(int a, int b) => a + b;
}