using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5154.Fixed;
[Observable]
public partial class Customer : INotifyPropertyChanged
{
  private string? _name;
  public string? Name
  {
    get
    {
      return _name;
    }
    set
    {
      if (!object.ReferenceEquals(value, _name))
      {
        _name = value;
        OnPropertyChanged("Name");
      }
    }
  }
  private string? _email;
  // Fixed: the property is no longer virtual.
  public string? Email
  {
    get
    {
      return _email;
    }
    set
    {
      if (!object.ReferenceEquals(value, _email))
      {
        _email = value;
        OnPropertyChanged("Email");
      }
    }
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}