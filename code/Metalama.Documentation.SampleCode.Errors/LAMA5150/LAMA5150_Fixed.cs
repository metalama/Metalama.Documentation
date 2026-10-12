// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System.ComponentModel;

namespace Doc.LAMA5150.Fixed;

public abstract class ObservableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // Fixed: the method is virtual, so the [Observable] aspect can override it.
    protected virtual void OnPropertyChanged( string propertyName )
        => this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
}

[Observable]
public partial class Customer : ObservableBase
{
    public string? Name { get; set; }
}
