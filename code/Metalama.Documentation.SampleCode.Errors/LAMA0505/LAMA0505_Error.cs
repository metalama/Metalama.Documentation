// This is public domain Metalama sample code.

namespace Doc.LAMA0505.Error;

// Error: the [Describe] aspect introduces an instance method into a static class.
[Describe]
internal static partial class MathHelper
{
    public static int Square( int x ) => x * x;
}
