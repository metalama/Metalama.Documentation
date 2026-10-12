// This is public domain Metalama sample code.

using Metalama.Framework.RunTime.Initialization;

namespace Doc.LAMA0550.Error;

[LogInitialized]
public partial class Document : IInitializable
{
    public string Title { get; init; } = "Untitled";

    // Error: the class isn't sealed, so Initialize must be public virtual.
    public void Initialize( InitializationContext context = default ) { }
}
