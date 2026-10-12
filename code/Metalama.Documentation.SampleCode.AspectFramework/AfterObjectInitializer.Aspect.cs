// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.AfterObjectInitializer;

public class PublishWhenInitializedAttribute : TypeAspect
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
        DomainEvents.Publish(
            new EntityInitialized( meta.Target.Type.Name, meta.This ) );
    }
}
