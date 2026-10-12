// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0277.Fixed;

public class TypeLoggerAttribute : TypeAspect
{
    [Introduce]
    public void LogType<T>()
    {
        // Fixed: the type is passed as a run-time value instead of a compile-time type argument.
        WriteTypeName( typeof(T) );
    }

    [Template]
    private void WriteTypeName( Type type )
    {
        Console.WriteLine( $"Type: {type.Name}" );
    }
}
