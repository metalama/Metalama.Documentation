// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0278.Fixed;

internal class Calculator
{
    public int Add( int a, int b ) => a + b;

    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // Because LogAspect is now a class, it can be added to the methods of the type.
            amender.SelectMany( t => t.Methods ).AddAspect<LogAspect>();
        }
    }
}
