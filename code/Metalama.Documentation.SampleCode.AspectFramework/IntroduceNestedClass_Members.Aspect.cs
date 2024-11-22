// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.IntroduceNestedClass_Members;

public class BuilderAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        // Introduce a nested type.
        var nestedType = builder.IntroduceClass( "Builder" );

        // Introduce properties.
        var properties =
            builder.Target.Properties.Where(
                p => p.Writeability != Writeability.None && !p.IsStatic );

        foreach ( var property in properties )
        {
            nestedType.IntroduceAutomaticProperty(
                property.Name,
                property.Type );
        }
    }
}