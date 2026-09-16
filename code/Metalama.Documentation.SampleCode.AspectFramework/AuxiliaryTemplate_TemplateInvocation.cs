// This is public domain Metalama sample code.

namespace Doc.AuxiliaryTemplate_TemplateInvocation;

public class SelfCachedClass
{
    [Cache]
    public int Add( int a, int b ) => a + b;

    [CacheAndRetry( IncludeRetry = true )]
    public int Rmove( int a, int b ) => a - b;
}