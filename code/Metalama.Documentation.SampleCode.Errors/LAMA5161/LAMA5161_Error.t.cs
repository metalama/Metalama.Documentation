// Warning LAMA5161 on `Person`: `The children of fields or properties of type 'Person' cannot be observed because the type does not implement INotifyPropertyChanged.`
using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5161.Error;
// Person does not implement INotifyPropertyChanged.
public class Person
{
  public string? Name { get; set; }
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
        _person = value;
        OnPropertyChanged("Person");
      }
    }
  }
  // Warning: changes of Person.Name cannot be observed.
  public string? DisplayName => this.Person.Name;
  private void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}