// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0204.Fixed;

// Fixed: the base type LogCategory now has a deserializing constructor.
[RunTimeOrCompileTime]
public class DatabaseLogCategory : LogCategory
{
    public int Level { get; }

    public DatabaseLogCategory( int level ) : base( "Database" )
    {
        this.Level = level;
    }
}
