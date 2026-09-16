// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.AdvisingTypeFabric;

internal class MyClass
{
    private class Fabric : TypeFabric
    {
        [Template]
        public int MethodTemplate( [CompileTime] int index )
        {
            return index;
        }

        public override void AmendType( ITypeAmender amender )
        {
            for ( var i = 0; i < 10; i++ )
            {
                amender.IntroduceMethod(
                    nameof(this.MethodTemplate),
                    args: new { index = i },
                    buildMethod: m => m.Name = "Method" + i );
            }
        }
    }
}