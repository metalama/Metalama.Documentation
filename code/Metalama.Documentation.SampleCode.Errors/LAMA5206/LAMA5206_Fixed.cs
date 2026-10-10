// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5206.Fixed;

public class Document
{
    public bool IsModified { get; set; }

    // Fixed: the name matches the can-execute method.
    [Command( CanExecuteMethod = nameof( CanSave ) )]
    private void Save() { }

    private bool CanSave() => this.IsModified;
}
