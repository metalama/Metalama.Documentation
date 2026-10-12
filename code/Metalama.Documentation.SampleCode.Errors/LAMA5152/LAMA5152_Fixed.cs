// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5152.Fixed;

// Fixed: Coordinates is an immutable struct that doesn't implement INotifyPropertyChanged.
public readonly record struct Coordinates( double Latitude );

[Observable]
public partial class Vehicle
{
    public Coordinates Position { get; set; }
}
