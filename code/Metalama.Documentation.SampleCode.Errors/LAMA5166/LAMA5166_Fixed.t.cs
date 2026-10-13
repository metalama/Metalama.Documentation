using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5166.Fixed;
// Fixed: the base class is observable.
[Observable]
public partial class Person : INotifyPropertyChanged
{
  private string _firstName = "";
  public string FirstName
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
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
[Observable]
public partial class Employee : Person
{
  public string Greeting => "Hello, " + this.FirstName;
  protected override void OnPropertyChanged(string propertyName)
  {
    switch (propertyName)
    {
      case "FirstName":
        OnPropertyChanged("Greeting");
        break;
    }
    base.OnPropertyChanged(propertyName);
  }
}