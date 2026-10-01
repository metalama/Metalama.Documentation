// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.Derived;

[Observable]
public class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}

public class PersonWithFullName : Person
{
    public string FullName => $"{this.FirstName} {this.LastName}";
}