// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.MixedInitialization;

public class TrackInitializationAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.OnInitialized),
            InitializerKind.AfterObjectInitializer );
    }

    [Template]
    private void OnInitialized()
    {
        Console.WriteLine( $"  Aspect: {meta.Target.Type.Name} fully initialized." );
    }
}
