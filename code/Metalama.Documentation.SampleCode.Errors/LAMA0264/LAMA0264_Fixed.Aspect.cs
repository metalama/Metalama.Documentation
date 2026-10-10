// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0264.Fixed;

public class AddLogMethodAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.Log), args: new { T = typeof(string) } );
    }

    [Template]
    public void Log<[CompileTime] T>(
        // Fixed: the parameter is run-time, so it becomes a parameter of the introduced method.
        T message )
    {
        Console.WriteLine( $"{meta.Target.Type.Name}: {message}" );
    }
}
