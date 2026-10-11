// This is public domain Metalama sample code.

namespace Doc.LAMA0091.Error;

// Error: [InterceptWrite] executes after the weaver of [Audit], so its interceptor cannot be registered.
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
