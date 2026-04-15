using System;
using Metalama.Framework.RunTime.Initialization;
namespace Doc.RecordWithExpression;
[ValidateAfterInitialization]
public sealed partial record Product(string Name, decimal Price) : IInitializable
{
  public void Initialize(InitializationContext context = default)
  {
    Console.WriteLine("Validating Product.");
  }
}
internal class Program
{
  private static void Main()
  {
    var product = new Product("Widget", 9.99m).WithInitialize();
    Console.WriteLine($"Created: {product}");
    var discounted = (product with
    {
      Price = 7.99m
    }
    ).WithInitialize(InitializationMetadata.Modify);
    Console.WriteLine($"Discounted: {discounted}");
  }
}