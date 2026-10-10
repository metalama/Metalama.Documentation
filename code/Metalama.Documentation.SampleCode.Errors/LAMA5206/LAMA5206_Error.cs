// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5206.Error;

public class Document
{
    public bool IsModified { get; set; }

    // Error: no method named CanSaveDocument exists.
    [Command( CanExecuteMethod = "CanSaveDocument" )]
    private void Save() { }

    private bool CanSave() => this.IsModified;
}
