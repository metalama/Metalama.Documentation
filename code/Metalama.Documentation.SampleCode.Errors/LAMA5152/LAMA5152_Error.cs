// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System.ComponentModel;

namespace Doc.LAMA5152.Error;

public struct Coordinates : INotifyPropertyChanged
{
    private double _latitude;

    public double Latitude
    {
        get => this._latitude;
        set
        {
            this._latitude = value;
            this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof(this.Latitude) ) );
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

[Observable]
public partial class Vehicle
{
    // Error: Coordinates is a struct that implements INotifyPropertyChanged.
    public Coordinates Position { get; set; }
}
