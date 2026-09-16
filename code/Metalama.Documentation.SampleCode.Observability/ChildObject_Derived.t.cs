using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.ChildObject_Derived;
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
        OnObservablePropertyChanged("Person", oldValue, (INotifyPropertyChanged? )value);
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
  [ObservedExpressions("Person")]
  protected virtual void OnObservablePropertyChanged(string propertyPath, INotifyPropertyChanged? oldValue, INotifyPropertyChanged? newValue)
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
[Observable]
public class PersonViewModelWithFullName : PersonViewModel
{
  public PersonViewModelWithFullName(Person model) : base(model)
  {
  }
  public string FullName => $"{this.FirstName} {this.LastName}, {this.Person.Title}";
  protected override void OnChildPropertyChanged(string parentPropertyPath, string propertyName)
  {
    switch (parentPropertyPath, propertyName)
    {
      case ("Person", "Title"):
        OnPropertyChanged("FullName");
        base.OnChildPropertyChanged(parentPropertyPath, propertyName);
        break;
    }
    base.OnChildPropertyChanged(parentPropertyPath, propertyName);
  }
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
      case "Person":
        OnPropertyChanged("FullName");
        break;
    }
    base.OnPropertyChanged(propertyName);
  }
}