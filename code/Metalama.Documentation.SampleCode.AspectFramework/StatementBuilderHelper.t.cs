namespace Doc.StatementBuilderHelper;
internal class Foo
{
  [NullGuard]
  public void DoSomething(string name, object? optional, int count)
  {
    if (name == null)
      throw new global::System.ArgumentNullException(nameof(name));
  }
}
