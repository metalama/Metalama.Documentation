using System;
using System.Collections.Generic;
namespace Doc.LAMA0655.Fixed;
[LogEquals]
public record Sensor(string Name)
{
  private event EventHandler? _changed;
  // The accessor templates of the aspect call meta.Proceed(), so the event keeps its backing field.
  public event EventHandler? Changed
  {
    add
    {
      Console.WriteLine("Accessing the event.");
      this._changed += value;
    }
    remove
    {
      Console.WriteLine("Accessing the event.");
      this._changed -= value;
    }
  }
  public virtual bool Equals(Sensor? other)
  {
    Console.WriteLine("Comparing two records.");
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.Name, other.Name) && EqualityComparer<EventHandler?>.Default.Equals(this._changed, other._changed));
  }
}