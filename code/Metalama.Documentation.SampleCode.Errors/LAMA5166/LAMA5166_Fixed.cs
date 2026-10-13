// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5166.Fixed;

// Fixed: the base class is observable.
[Observable]
public partial class Person
{
    public string FirstName { get; set; } = "";
}

[Observable]
public partial class Employee : Person
{
    public string Greeting => "Hello, " + this.FirstName;
}
