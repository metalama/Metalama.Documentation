// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.ComponentModel;

namespace Doc.LAMA5202.Error;

public partial class DocumentViewModel : INotifyPropertyChanged
{
    private bool _isDirty = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    // Warning: the can-execute property is not public.
    private bool IsSaveEnabled => this._isDirty;

    [Command]
    public void Save() => this._isDirty = false;
}
