// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5204.Fixed;

public partial class DocumentViewModel
{
    private int _unsavedChanges = 1;

    // Fixed: the can-execute method returns bool.
    private bool CanSave() => this._unsavedChanges > 0;

    [Command]
    public void Save() => this._unsavedChanges = 0;
}
