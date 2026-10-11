// Warning LAMA5166 on `FirstName`: `The 'Person.FirstName' property cannot be observed because 'Person' does not implement INotifyPropertyChanged. Consider implementing the INotifyPropertyChanged interface in 'Person', marking 'Person' with [Constant], or using ConfigureObservability via a fabric.`
using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5166.Error;
// This base class does not implement INotifyPropertyChanged.
public class Person
{
  public string FirstName { get; set; } = "";
}
[Observable]
public partial class Employee : Person, INotifyPropertyChanged
{
  // Warning: changes to FirstName cannot be observed.
  public string Greeting => "Hello, " + this.FirstName;
  protected virtual void OnPropertyChanged(string propertyName)
  {
    switch (propertyName)
    {
      case "FirstName":
        OnPropertyChanged("Greeting");
        break;
    }
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}