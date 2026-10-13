using System.ComponentModel;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Observability.Configuration;
namespace Doc.ChildObject_Derived_Preemptive;
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
  private string? _title;
  public string? Title
  {
    get
    {
      return _title;
    }
    set
    {
      if (!object.ReferenceEquals(value, _title))
      {
        _title = value;
        OnPropertyChanged("Title");
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
  private PropertyChangedEventHandler? _handlePersonPropertyChanged;
  [ObservedExpressions("Person")]
  protected virtual void OnChildPropertyChanged(string parentPropertyPath, string propertyName)
  {
  }
  protected virtual void OnPropertyChanged(string propertyName)
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
            OnChildPropertyChanged("Person", "FirstName");
            break;
          case "LastName":
            OnPropertyChanged("LastName");
            OnChildPropertyChanged("Person", "LastName");
            break;
          default:
            OnChildPropertyChanged("Person", propertyName);
            break;
        }
      }
    }
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}