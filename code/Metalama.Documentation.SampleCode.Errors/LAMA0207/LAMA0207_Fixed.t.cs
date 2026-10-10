using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using System;
namespace Doc.LAMA0207.Fixed;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[RunTimeOrCompileTime]
public class LogCategory : ICompileTimeSerializable
{
  public string Name { get; set; } = "Default";
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  public class Serializer : ReferenceTypeSerializer
  {
    private readonly string _key;
    public Serializer(string key)
    {
      this._key = key;
    }
    // Fixed: a parameterless constructor that derived serializers can call.
    public Serializer() : this("Name")
    {
    }
    public override object CreateInstance(Type type, IArgumentsReader constructorArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void SerializeObject(object obj, IArgumentsWriter constructorArguments, IArgumentsWriter initializationArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    public override void DeserializeFields(object obj, IArgumentsReader initializationArguments) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
[RunTimeOrCompileTime]
public class DatabaseLogCategory : LogCategory
{
  public int Level { get; set; }
  public DatabaseLogCategory()
  {
    this.Name = "Database";
  }
}