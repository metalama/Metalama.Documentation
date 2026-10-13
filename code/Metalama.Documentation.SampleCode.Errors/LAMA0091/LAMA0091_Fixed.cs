// This is public domain Metalama sample code.

namespace Doc.LAMA0091.Fixed;

// Fixed: both interceptor aspects are ordered before [Audit].
[InterceptWrite]
[Audit]
[InterceptPrint]
internal static class Program
{
    public static void Run()
    {
        Target.Print( "a" );
        Target.Write( "b" );
    }
}
