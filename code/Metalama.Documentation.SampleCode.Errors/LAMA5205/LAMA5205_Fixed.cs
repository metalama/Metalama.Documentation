// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5205.Fixed;

public class Document
{
    public bool IsModified { get; set; }

    [Command]
    private void Save() { }

    // Fixed: only one method matches the can-execute role.
    private bool CanSave() => this.IsModified;
}
