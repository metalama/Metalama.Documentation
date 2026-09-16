// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;

namespace Metalama.Documentation.SampleCode.Immutability.Warning;

[Immutable]
public class Person
{
    public required string FirstName { get; set; }

    public required string LastName { get; init; }
}