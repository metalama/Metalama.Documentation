// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceTopLevelClass;

public class BuilderAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        var builderType = builder
            .With( builder.Target.GetNamespace()! )
            .WithChildNamespace( "Builders" )
            .IntroduceClass( builder.Target.Name + "Builder" );
    }
}