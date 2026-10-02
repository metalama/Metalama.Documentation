using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Doc.StructurallyComparable
{
  [StructuralEquatable]
  class WineBottle
  {
    public string Cepage { get; init; }
    public int Millesime { get; init; }
    public WineProducer Vigneron { get; init; }
    public override bool Equals(object? other)
    {
      if (!EqualityComparer<string>.Default.Equals(this.Cepage, ((WineBottle)other!).Cepage))
      {
        return false;
      }
      if (!EqualityComparer<int>.Default.Equals(this.Millesime, ((WineBottle)other!).Millesime))
      {
        return false;
      }
      if (!EqualityComparer<WineProducer>.Default.Equals(this.Vigneron, ((WineBottle)other!).Vigneron))
      {
        return false;
      }
      return true;
    }
    public override int GetHashCode()
    {
      var hashCode = new HashCode();
      hashCode.Add(this.Cepage);
      hashCode.Add(this.Millesime);
      hashCode.Add(this.Vigneron);
      return hashCode.ToHashCode();
    }
  }
  [StructuralEquatable]
  class WineProducer
  {
    public string Name { get; init; }
    public string Address { get; init; }
    public override bool Equals(object? other)
    {
      if (!EqualityComparer<string>.Default.Equals(this.Name, ((WineProducer)other!).Name))
      {
        return false;
      }
      if (!EqualityComparer<string>.Default.Equals(this.Address, ((WineProducer)other!).Address))
      {
        return false;
      }
      return true;
    }
    public override int GetHashCode()
    {
      var hashCode = new HashCode();
      hashCode.Add(this.Name);
      hashCode.Add(this.Address);
      return hashCode.ToHashCode();
    }
  }
}