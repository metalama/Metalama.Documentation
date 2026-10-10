using System;
namespace Doc.LAMA0650.Fixed;
public class Vehicle
{
  public string Model { get; init; } = "";
  public virtual string Describe() => $"Vehicle {this.Model}";
}
public class Car : Vehicle
{
  [LogComparison]
  public bool IsSameModel(Car other)
  {
    var otherDescription = other.Describe();
    Console.WriteLine($"Comparing with {otherDescription}.");
    return this.Model == other.Model;
  }
}