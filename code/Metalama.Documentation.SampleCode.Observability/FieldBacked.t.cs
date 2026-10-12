using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.FieldBacked;
[Observable]
public class Person : INotifyPropertyChanged
{
  private string? _firstName1;
  private string? _firstName
  {
    get
    {
      return _firstName1;
    }
    set
    {
      if (!object.ReferenceEquals(value, _firstName1))
      {
        _firstName1 = value;
        OnPropertyChanged("FirstName");
      }
    }
  }
  private string? _lastName1;
  private string? _lastName
  {
    get
    {
      return _lastName1;
    }
    set
    {
      if (!object.ReferenceEquals(value, _lastName1))
      {
        _lastName1 = value;
        OnPropertyChanged("LastName");
      }
    }
  }
  public string? FirstName
  {
    get
    {
      return FirstName_Source;
    }
    set
    {
      if (!object.ReferenceEquals(value, FirstName_Source))
      {
        FirstName_Source = value;
        OnPropertyChanged("FirstName");
      }
    }
  }
  private string? FirstName_Source { get => this._firstName; set => this._firstName = value; }
  public string? LastName
  {
    get
    {
      return LastName_Source;
    }
    set
    {
      if (!object.ReferenceEquals(value, LastName_Source))
      {
        LastName_Source = value;
        OnPropertyChanged("LastName");
      }
    }
  }
  private string? LastName_Source { get => this._lastName; set => this._lastName = value; }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}