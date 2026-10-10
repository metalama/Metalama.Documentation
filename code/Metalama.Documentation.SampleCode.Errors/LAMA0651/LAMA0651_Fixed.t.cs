using System;
namespace Doc.LAMA0651.Fixed;
[CustomHashCode]
public record Customer
{
  public string Name { get; init; } = "";
  public string Email { get; init; } = "";
  public override int GetHashCode()
  {
    Console.WriteLine("Computing the hash code of Customer.");
    var hashCode = new HashCode();
    hashCode.Add(Name);
    hashCode.Add(Email);
    return hashCode.ToHashCode();
  }
}