using System;
using Metalama.Framework.RunTime;
namespace Doc.IntroduceRequiredParameter;
[AddTimestamp]
internal class Order
{
  public Order(int id, DateTime creationTime)
  {
    this.Id = id;
  }
  public Order(int id, string label, DateTime creationTime) : this(id, creationTime)
  {
    this.Label = label;
  }
  public int Id { get; }
  public string? Label { get; }
  [SourceCompatibilityConstructor]
  public Order(int id) : this(id: id, creationTime: DateTime.Now)
  {
  }
  [SourceCompatibilityConstructor]
  public Order(int id, string label) : this(id: id, label: label, creationTime: DateTime.Now)
  {
  }
}
