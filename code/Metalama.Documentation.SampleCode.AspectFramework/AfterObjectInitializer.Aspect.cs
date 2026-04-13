// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.AfterObjectInitializer;

public class ValidateAfterInitializationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.Validate),
            InitializerKind.AfterObjectInitializer );
    }

    [Template]
    private void Validate()
    {
        Console.WriteLine( $"Validating {meta.Target.Type.Name}." );
    }
}
