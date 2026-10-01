// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.ChildObject_Derived;

[Observable]
public class PersonViewModel
{
    public Person Person { get; set; }

    public PersonViewModel( Person model )
    {
        this.Person = model;
    }

    public string? FirstName => this.Person.FirstName;

    public string? LastName => this.Person.LastName;
}

[Observable]
public class PersonViewModelWithFullName : PersonViewModel
{
    public PersonViewModelWithFullName( Person model ) : base( model ) { }

    public string FullName => $"{this.FirstName} {this.LastName}, {this.Person.Title}";
}