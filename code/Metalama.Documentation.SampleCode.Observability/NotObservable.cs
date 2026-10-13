// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System;

namespace Doc.NotObservable;

[Observable]
public class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [NotObservable]
    public DateTime LastWriteTime { get; set; }
}