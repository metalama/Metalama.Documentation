// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5205.Error;

public class Document
{
    public bool IsModified { get; set; }

    [Command]
    private void Save() { }

    // Error: the overloads of CanSave are both valid can-execute methods.
    private bool CanSave() => this.IsModified;

    private bool CanSave( int parameter ) => parameter > 0;
}
