// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.ComputedProperty;

[Observable]
public class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName => $"{this.FirstName} {this.LastName}";
}