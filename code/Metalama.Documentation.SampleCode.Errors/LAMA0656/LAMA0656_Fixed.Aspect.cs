// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0656.Fixed;

public class LogInitializedAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Call the template after the object initializer of each new instance.
        builder.AddInitializer(
            nameof(this.OnInitialized),
            InitializerKind.AfterObjectInitializer );
    }

    [Template]
    private void OnInitialized()
    {
        Console.WriteLine( $"{meta.Target.Type.Name} initialized." );
    }
}
