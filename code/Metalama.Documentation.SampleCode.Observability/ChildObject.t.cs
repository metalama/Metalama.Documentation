using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.ChildObject;
[Observable]
public sealed class PersonViewModel : INotifyPropertyChanged
{
  private Person _person = default !;
  public Person Person
  {
    get
    {
      return _person;
    }
    set
    {
      if (!object.ReferenceEquals(value, _person))
      {
        var oldValue = _person;
        if (oldValue != null)
        {
          oldValue.PropertyChanged -= _handlePersonPropertyChanged;
        }
        _person = value;
        OnPropertyChanged("FirstName");
        OnPropertyChanged("FullName");
        OnPropertyChanged("LastName");
        OnPropertyChanged("Person");
        SubscribeToPerson(value);
      }
    }
  }
  public PersonViewModel(Person model)
  {
    this.Person = model;
  }
  public string? FirstName => this.Person.FirstName;
  public string? LastName => this.Person.LastName;
  public string FullName => $"{this.FirstName} {this.LastName}";
  private PropertyChangedEventHandler? _handlePersonPropertyChanged;
  private void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  private void SubscribeToPerson(Person value)
  {
    if (value != null)
    {
      _handlePersonPropertyChanged ??= HandlePropertyChanged;
      value.PropertyChanged += _handlePersonPropertyChanged;
    }
    void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      {
        var propertyName = e.PropertyName;
        switch (propertyName)
        {
          case "FirstName":
            OnPropertyChanged("FirstName");
            OnPropertyChanged("FullName");
            break;
          case "LastName":
            OnPropertyChanged("FullName");
            OnPropertyChanged("LastName");
            break;
        }
      }
    }
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}