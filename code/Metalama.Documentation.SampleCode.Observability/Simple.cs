// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.Simple;

[Observable]
public class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}