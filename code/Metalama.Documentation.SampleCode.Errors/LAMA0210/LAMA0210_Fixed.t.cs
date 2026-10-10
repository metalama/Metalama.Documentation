using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;
namespace Doc.LAMA0210.Fixed;
[RunTimeOrCompileTime]
public record RetryPolicy(int MaxAttempts) : ICompileTimeSerializable
{
  // The record now provides its own serializer.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  public class Serializer : ReferenceTypeSerializer<RetryPolicy>
  {
    public override RetryPolicy CreateInstance(IArgumentsReader constructorArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void SerializeObject(RetryPolicy obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}
public class OrderService
{
  [Retry(3)]
  public void PlaceOrder(int orderId)
  {
    for (var attempt = 1;; attempt++)
    {
      try
      {
        Console.WriteLine($"Placing order {orderId}.");
        return;
      }
      catch (Exception)when (attempt < 3)
      {
        Console.WriteLine($"Attempt {attempt} failed. Retrying.");
      }
    }
  }
}