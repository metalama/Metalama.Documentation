// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0550.Error;

public class LogInitializedAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Adds the template to the Initialize method, which runs after the object initializer.
        builder.AddInitializer( nameof(this.LogInitialization), InitializerKind.AfterObjectInitializer );
    }

    [Template]
    private void LogInitialization()
    {
        Console.WriteLine( $"{meta.Target.Type.Name} initialized." );
    }
}
