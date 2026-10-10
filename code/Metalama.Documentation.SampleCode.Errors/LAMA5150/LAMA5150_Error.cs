// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System.ComponentModel;

namespace Doc.LAMA5150.Error;

public abstract class ObservableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // This method is not virtual, so the [Observable] aspect can't override it.
    protected void OnPropertyChanged( string propertyName )
        => this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
}

// Error: the base class doesn't define an overridable OnPropertyChanged method.
[Observable]
public partial class Customer : ObservableBase
{
    public string? Name { get; set; }
}
