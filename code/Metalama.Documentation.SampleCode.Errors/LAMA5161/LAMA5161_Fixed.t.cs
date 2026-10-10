using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5161.Fixed;
// Fixed: Person implements INotifyPropertyChanged thanks to the [Observable] aspect.
[Observable]
public partial class Person : INotifyPropertyChanged
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
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
[Observable]
public sealed partial class PersonViewModel : INotifyPropertyChanged
{
  public PersonViewModel(Person person)
  {
    this.Person = person;
  }
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
        OnPropertyChanged("DisplayName");
        OnPropertyChanged("Person");
        SubscribeToPerson(value);
      }
    }
  }
  public string? DisplayName => this.Person.Name;
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
          case "Name":
            OnPropertyChanged("DisplayName");
            break;
        }
      }
    }
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}