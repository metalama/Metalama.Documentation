// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.MixedInitialization;

public class TrackLifecycleAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.AddInitializer(
            nameof(this.OnBeforeConstruction),
            InitializerKind.BeforeInstanceConstructor );

        builder.AddInitializer(
            nameof(this.OnConstructed),
            InitializerKind.AfterLastInstanceConstructor );

        builder.AddInitializer(
            nameof(this.OnInitialized),
            InitializerKind.AfterObjectInitializer );
    }

    [Template]
    private void OnBeforeConstruction()
        => LifecycleRegistry.SetState( meta.This, LifecycleState.BeingConstructed );

    [Template]
    private void OnConstructed()
        => LifecycleRegistry.SetState( meta.This, LifecycleState.Constructed );

    [Template]
    private void OnInitialized()
        => LifecycleRegistry.SetState( meta.This, LifecycleState.FullyInitialized );
}
