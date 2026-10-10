// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0277.Error;

public class TypeLoggerAttribute : TypeAspect
{
    [Introduce]
    public void LogType<T>()
    {
        // Error: T is a type parameter of the introduced method, so it is only known at run time.
        WriteTypeName<T>();
    }

    [Template]
    private void WriteTypeName<[CompileTime] T>()
    {
        Console.WriteLine( $"Type: {typeof(T).Name}" );
    }
}
