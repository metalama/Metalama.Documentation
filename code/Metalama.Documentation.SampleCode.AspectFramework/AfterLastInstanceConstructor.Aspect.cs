// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.AfterLastInstanceConstructor;

public class PublishWhenCreatedAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.OnCreated),
            InitializerKind.AfterLastInstanceConstructor );
    }

    [Template]
    private void OnCreated()
    {
        DomainEvents.Publish(
            new EntityCreated( meta.Target.Type.Name, meta.This ) );
    }
}
