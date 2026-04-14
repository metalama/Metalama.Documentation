// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.IntroduceExtensionBlock;

public class GenerateToDisplayStringAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Introduce a top-level static class named <TargetType>Extensions.
        var ns = builder.With( builder.Target.Compilation )
            .WithNamespace( builder.Target.ContainingNamespace.FullName );

        var extensionsClass = ns.IntroduceClass(
            builder.Target.Name + "Extensions",
            buildType: t => t.IsStatic = true );

        // Introduce an instance extension block for the target enum type.
        var extensionBlock =
            extensionsClass.IntroduceExtensionBlock( builder.Target, "self" );

        // Introduce the ToDisplayString method into the extension block.
        extensionBlock.IntroduceMethod( nameof(ToDisplayString) );
    }

    [Template]
    public string ToDisplayString()
    {
        // A complete implementation would use SwitchStatementBuilder to generate
        // a switch expression mapping each member to a display string.
        // See the "Generating switch statements" article for details.
        return "unknown";
    }
}
