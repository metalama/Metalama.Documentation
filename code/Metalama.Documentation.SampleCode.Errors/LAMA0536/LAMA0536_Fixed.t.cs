using System;
namespace Doc.LAMA0536.Fixed;
// The [AddTimestamp] aspect adds a creationTime parameter and a forwarding constructor.
[AddTimestamp]
internal class Order
{
  public Order(int id, DateTime creationTime)
  {
    this.Id = id;
  }
  public int Id { get; }
  public Order(int id) : this(id: id, creationTime: DateTime.Now)
  {
  }
}