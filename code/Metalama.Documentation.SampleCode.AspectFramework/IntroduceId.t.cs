using System;
namespace Doc.IntroduceId;
[IntroduceId]
internal class MyClass
{
  public Guid Id { get; } = Guid.NewGuid();
}