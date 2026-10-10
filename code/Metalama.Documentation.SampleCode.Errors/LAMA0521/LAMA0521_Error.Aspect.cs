// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.Diagnostics;

namespace Doc.LAMA0521.Error;

public class DefaultDebuggerDisplayAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: the default strategy fails when the type already has a [DebuggerDisplay] attribute.
        builder.IntroduceAttribute(
            AttributeConstruction.Create(
                typeof(DebuggerDisplayAttribute),
                constructorArguments: new object?[] { "{Name}" } ) );
    }
}
