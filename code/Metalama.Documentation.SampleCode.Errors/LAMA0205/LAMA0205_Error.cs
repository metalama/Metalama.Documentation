// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;

namespace Doc.LAMA0205.Error;

[RunTimeOrCompileTime]
public class NamedItem
{
    public string Name { get; }

    public NamedItem( string name )
    {
        this.Name = name;
    }
}

// Error: the base type NamedItem is not serializable and has no parameterless constructor.
[RunTimeOrCompileTime]
public class LogCategory : NamedItem, ICompileTimeSerializable
{
    public int Level { get; }

    public LogCategory( string name, int level ) : base( name )
    {
        this.Level = level;
    }
}
