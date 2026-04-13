// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceExtensionBlock;

public class AddStringExtensionsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Introduce an instance extension block for string.
        var extensionBlock = builder.IntroduceExtensionBlock( typeof(string), "self" );

        // Introduce a method into the extension block.
        extensionBlock.IntroduceMethod( nameof(IsNullOrBlank) );
    }

    [Template]
    public bool IsNullOrBlank()
    {
        return false;
    }
}
