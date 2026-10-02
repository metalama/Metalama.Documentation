using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.Derived;
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
public class PersonWithFullName : Person
{
  public string FullName => $"{this.FirstName} {this.LastName}";
  protected override void OnPropertyChanged(string propertyName)
  {
    switch (propertyName)
    {
      case "FirstName":
        OnPropertyChanged("FullName");
        break;
      case "LastName":
        OnPropertyChanged("FullName");
        break;
    }
    base.OnPropertyChanged(propertyName);
  }
}