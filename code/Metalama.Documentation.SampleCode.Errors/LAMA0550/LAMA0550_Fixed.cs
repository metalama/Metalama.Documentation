// This is public domain Metalama sample code.

using Metalama.Framework.RunTime.Initialization;

namespace Doc.LAMA0550.Fixed;

[LogInitialized]
public partial class Document : IInitializable
{
    public string Title { get; init; } = "Untitled";

    // Fixed: Initialize is virtual, so derived types can extend the initialization.
    public virtual void Initialize( InitializationContext context = default ) { }
}
