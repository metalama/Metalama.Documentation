// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0271.Error;

[RunTimeOrCompileTime]
internal static class ObjectExtensions
{
    // Error: a template cannot be an extension method.
    [Template]
    public static void PrintDetails( this object self )
    {
        Console.WriteLine( self );
    }
}
