// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5204.Error;

public partial class DocumentViewModel
{
    private int _unsavedChanges = 1;

    // Error: a can-execute method must return bool.
    private int CanSave() => this._unsavedChanges;

    [Command]
    public void Save() => this._unsavedChanges = 0;
}
