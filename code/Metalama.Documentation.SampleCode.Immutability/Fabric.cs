// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;
using System;

namespace Metalama.Documentation.SampleCode.Immutability.Fabric;

[Immutable( ImmutabilityKind.Deep )]
public class Person
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public Uri? HomePage { get; init; }
}