// This is public domain Metalama sample code.

namespace Doc.LAMA0119.Error;

// Warning: the file contains compile-time code but no using directive for a Metalama.Framework namespace.
[Metalama.Framework.Aspects.CompileTime]
internal static class LogFormatter
{
    public static string GetEntryMessage( string methodName ) => $"Entering {methodName}.";
}
