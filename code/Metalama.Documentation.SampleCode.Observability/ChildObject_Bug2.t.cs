using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.ChildObject_Bug2;
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
[Observable]
public class PersonViewModel : INotifyPropertyChanged
{
  public Person Person { get; }
  public PersonViewModel(Person model)
  {
    this.Person = model;
  }
  public string? FirstName => this.Person.FirstName;
  public string? LastName => this.Person.LastName;
  public string FullName => $"{this.FirstName} {this.LastName}";
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}