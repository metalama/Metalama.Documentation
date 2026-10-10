// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0551.Fixed;

public class LogConstructedAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Adds the template to the OnConstructed method, which runs after the last constructor.
        builder.AddInitializer( nameof(this.LogConstruction), InitializerKind.AfterLastInstanceConstructor );
    }

    [Template]
    private void LogConstruction()
    {
        Console.WriteLine( $"{meta.Target.Type.Name} constructed." );
    }
}
