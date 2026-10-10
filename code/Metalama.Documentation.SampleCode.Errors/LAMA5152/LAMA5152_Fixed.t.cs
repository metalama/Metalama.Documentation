using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5152.Fixed;
// Fixed: Coordinates is an immutable struct that doesn't implement INotifyPropertyChanged.
public readonly record struct Coordinates(double Latitude);
[Observable]
public partial class Vehicle : INotifyPropertyChanged
{
  private Coordinates _position;
  public Coordinates Position
  {
    get
    {
      return _position;
    }
    set
    {
      if (_position != value)
      {
        _position = value;
        OnPropertyChanged("Position");
      }
    }
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}