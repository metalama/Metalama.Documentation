// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5166.Error;

// This base class does not implement INotifyPropertyChanged.
public class Person
{
    public string FirstName { get; set; } = "";
}

[Observable]
public partial class Employee : Person
{
    // Warning: changes to FirstName cannot be observed.
    public string Greeting => "Hello, " + this.FirstName;
}
