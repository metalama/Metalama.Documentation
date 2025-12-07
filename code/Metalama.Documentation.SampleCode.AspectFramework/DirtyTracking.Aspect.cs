// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.DirtyTracking;

public class DirtyTrackingAttribute : TypeAspect
{
    // Introduces a property to track whether the object has been modified.
    [Introduce]
    public bool IsDirty { get; private set; }

    // Introduces OnPropertyChanged if not present in base class, or overrides it if present.
    // When overriding, meta.Proceed() calls the base implementation.
    [Introduce( WhenExists = OverrideStrategy.Override )]
    protected virtual void OnPropertyChanged( string propertyName )
    {
        this.IsDirty = true;
        meta.Proceed();
    }
}
