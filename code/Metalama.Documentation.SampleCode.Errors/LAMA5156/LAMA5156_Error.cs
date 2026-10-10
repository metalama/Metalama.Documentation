// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System.ComponentModel;

namespace Doc.LAMA5156.Error;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // The only OnPropertyChanged method takes a PropertyChangedEventArgs.
    protected virtual void OnPropertyChanged( PropertyChangedEventArgs args )
    {
        this.PropertyChanged?.Invoke( this, args );
    }
}

// Error: no OnPropertyChanged(string) method can raise the notifications.
[Observable]
public partial class CustomerViewModel : ViewModelBase
{
    public string? Name { get; set; }
}
