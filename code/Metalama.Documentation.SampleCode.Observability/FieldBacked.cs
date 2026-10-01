// This is public domain Metalama sample code.

// ReSharper disable ConvertToAutoProperty

using Metalama.Patterns.Observability;

namespace Doc.FieldBacked;

[Observable]
public class Person
{
    private string? _firstName;
    private string? _lastName;

    public string? FirstName
    {
        get => this._firstName;
        set => this._firstName = value;
    }

    public string? LastName
    {
        get => this._lastName;
        set => this._lastName = value;
    }
}