// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceNestedClass_BaseClass;

public class BuilderAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        base.BuildAspect( builder );

        builder.IntroduceClass(
            "Builder",
            buildType:
            typeBuilder =>
            {
                typeBuilder.IsSealed = false;
                typeBuilder.BaseType = (INamedType) TypeFactory.GetType( typeof(BaseFactory) );
            } );
    }
}