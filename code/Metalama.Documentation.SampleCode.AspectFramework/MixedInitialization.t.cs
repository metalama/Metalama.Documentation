using System;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.MixedInitialization;
[TrackInitialization]
public sealed partial class Customer : IInitializable
{
  public Customer(int id)
  {
    this.Id = id;
    Console.WriteLine($"  Constructor: Id = {this.Id}");
  }
  public int Id { get; }
  public string Name { get; init; } = "";
  public string Email { get; init; } = "";
  public void Initialize(InitializationContext context = default)
  {
    Console.WriteLine("  Aspect: Customer fully initialized.");
    Console.WriteLine($"  User code: validating {this.Name} ({this.Email}).");
  }
}
internal class Program
{
  private static void Main()
  {
    Console.WriteLine("Creating customer:");
    var customer = new Customer(1)
    {
      Name = "Alice",
      Email = "alice@example.com"
    }.WithInitialize();
    Console.WriteLine($"  Result: {customer.Name} ({customer.Email})");
  }
}
