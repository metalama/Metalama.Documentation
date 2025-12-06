// This is public domain Metalama sample code.

#pragma warning disable CA1725

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.IntroduceNestedClass;

[Inheritable]
public class BuilderAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        // Find the Builder class of the base class, if any.
        var baseBuilderClass =
            builder.Target.BaseType?.Types.OfName( "Builder" ).SingleOrDefault();

        // Introduce a public nested type.
        builder.IntroduceClass(
            "Builder",
            OverrideStrategy.New,
            buildType:
            type =>
            {
                type.Accessibility = Accessibility.Public;
                type.BaseType = baseBuilderClass;
            } );
    }
}