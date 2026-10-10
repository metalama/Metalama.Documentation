using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
namespace Doc.LAMA0205.Fixed;
// Fixed: the base type is now compile-time serializable, so its Name property is serialized too.
[RunTimeOrCompileTime]
public class NamedItem : ICompileTimeSerializable
{
  public string Name { get; }
  public NamedItem(string name)
  {
    this.Name = name;
  }
}
[RunTimeOrCompileTime]
public class LogCategory : NamedItem, ICompileTimeSerializable
{
  public int Level { get; }
  public LogCategory(string name, int level) : base(name)
  {
    this.Level = level;
  }
}