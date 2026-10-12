using System;
using System.Collections.Generic;
namespace Doc.LAMA0552.Fixed;
[TraceEquals]
internal record Order(int Id)
{
  public virtual bool Equals(Order? other)
  {
    Console.WriteLine("Comparing.");
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.Id, other.Id));
  }
}