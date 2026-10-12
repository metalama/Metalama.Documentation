// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.Diagnostics;

namespace Doc.LAMA0521.Fixed;

public class DefaultDebuggerDisplayAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Fixed: an existing [DebuggerDisplay] attribute is kept.
        builder.IntroduceAttribute(
            AttributeConstruction.Create(
                typeof(DebuggerDisplayAttribute),
                constructorArguments: new object?[] { "{Name}" } ),
            whenExists: OverrideStrategy.Ignore );
    }
}
