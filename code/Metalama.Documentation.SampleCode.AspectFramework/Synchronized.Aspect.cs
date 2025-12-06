// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.Synchronized;

internal class SynchronizedAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var method in builder.Target.Methods.Where( m => !m.IsStatic ) )
        {
            builder.With( method ).Override( nameof(this.OverrideMethod) );
        }
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        lock ( meta.This )
        {
            return meta.Proceed();
        }
    }
}