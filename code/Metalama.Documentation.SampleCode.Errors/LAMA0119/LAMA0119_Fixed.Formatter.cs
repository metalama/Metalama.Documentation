// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0119.Fixed;

// Fixed: the file imports the Metalama.Framework.Aspects namespace.
[CompileTime]
internal static class LogFormatter
{
    public static string GetEntryMessage( string methodName ) => $"Entering {methodName}.";
}
