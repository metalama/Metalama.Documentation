using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;
namespace Doc.LAMA0208.Fixed;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[RunTimeOrCompileTime]
public class RetryPolicy : ICompileTimeSerializable
{
  public int MaxAttempts { get; set; } = 3;
  // Fixed: the type has a single nested serializer.
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  public class Serializer : ReferenceTypeSerializer
  {
    public override object CreateInstance(Type type, IArgumentsReader constructorArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void SerializeObject(object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void DeserializeFields(object obj, IArgumentsReader initializationArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}