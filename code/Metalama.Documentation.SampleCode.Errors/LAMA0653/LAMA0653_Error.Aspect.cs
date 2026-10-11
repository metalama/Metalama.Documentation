// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.LAMA0653.Error;

public class TrackQuantityAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(IntroducedEquals), whenExists: OverrideStrategy.Override, args: new { T = builder.Target } );
        builder.With( builder.Target.Properties.Single( p => p.Name == "Quantity" ) ).Override( nameof(QuantityTemplate) );
    }

    [Template( Name = "Equals" )]
    public bool IntroducedEquals<[CompileTime] T>( T? other )
    {
        return meta.Proceed();
    }

    [Template]
    public dynamic? QuantityTemplate
    {
        get
        {
            // The template does not call the original implementation, so the property loses its backing field.
            return 1;
        }

        set { }
    }
}
