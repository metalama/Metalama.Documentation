// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0204.Error;

// Error: the base type LogCategory, declared in a referenced project, has no constructor that Metalama can call.
[RunTimeOrCompileTime]
public class DatabaseLogCategory : LogCategory
{
    public int Level { get; }

    public DatabaseLogCategory( int level ) : base( "Database" )
    {
        this.Level = level;
    }
}
