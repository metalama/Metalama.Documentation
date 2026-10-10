// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5201.Error;

public partial class DocumentViewModel
{
    private bool _isDirty = true;

    private bool CanSave() => this._isDirty;

    private bool IsSaveEnabled => this._isDirty;

    // Error: the attribute sets both CanExecuteMethod and CanExecuteProperty.
    [Command( CanExecuteMethod = "CanSave", CanExecuteProperty = "IsSaveEnabled" )]
    public void Save() => this._isDirty = false;
}
