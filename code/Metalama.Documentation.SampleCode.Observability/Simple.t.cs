using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.Simple;
[Observable]
public class Person : INotifyPropertyChanged
{
  private string? _firstName;
  public string? FirstName
  {
    get
    {
      return _firstName;
    }
    set
    {
      if (!object.ReferenceEquals(value, _firstName))
      {
        _firstName = value;
        OnPropertyChanged("FirstName");
      }
    }
  }
  private string? _lastName;
  public string? LastName
  {
    get
    {
      return _lastName;
    }
    set
    {
      if (!object.ReferenceEquals(value, _lastName))
      {
        _lastName = value;
        OnPropertyChanged("LastName");
      }
    }
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}