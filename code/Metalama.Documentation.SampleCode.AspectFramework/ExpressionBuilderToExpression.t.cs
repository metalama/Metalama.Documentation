using System.Threading;
namespace Doc.ExpressionBuilderToExpression;
[AutoIncrementIdAspect]
internal class Foo
{
  public void DoSomething()
  {
  }
  private readonly int _id = Interlocked.Increment(ref _nextId);
  private static int _nextId;
}
