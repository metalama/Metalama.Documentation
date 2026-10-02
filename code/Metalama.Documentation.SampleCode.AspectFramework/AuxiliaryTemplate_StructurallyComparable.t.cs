using System;
using System.Collections.Generic;
namespace Doc.StructurallyComparable;
[StructuralEquatable]
internal class WineBottle
{
  public string Cepage { get; init; }
  public int Millesime { get; init; }
  public WineProducer Vigneron { get; init; }
  public override bool Equals(object? other)
  {
    if (!EqualityComparer<string>.Default.Equals(Cepage, ((WineBottle)other).Cepage))
    {
      return false;
    }
    if (!EqualityComparer<int>.Default.Equals(Millesime, ((WineBottle)other).Millesime))
    {
      return false;
    }
    if (!EqualityComparer<WineProducer>.Default.Equals(Vigneron, ((WineBottle)other).Vigneron))
    {
      return false;
    }
    return true;
  }
  public override int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(Cepage);
    hashCode.Add(Millesime);
    hashCode.Add(Vigneron);
    return hashCode.ToHashCode();
  }
}
[StructuralEquatable]
internal class WineProducer
{
  public string Name { get; init; }
  public string Address { get; init; }
  public override bool Equals(object? other)
  {
    if (!EqualityComparer<string>.Default.Equals(Name, ((WineProducer)other).Name))
    {
      return false;
    }
    if (!EqualityComparer<string>.Default.Equals(Address, ((WineProducer)other).Address))
    {
      return false;
    }
    return true;
  }
  public override int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(Name);
    hashCode.Add(Address);
    return hashCode.ToHashCode();
  }
}