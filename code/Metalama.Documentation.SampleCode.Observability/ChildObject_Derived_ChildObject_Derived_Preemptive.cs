// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Observability.Configuration;

namespace Doc.ChildObject_Derived_Preemptive;

[Observable]
public class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Title { get; set; }
}

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

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureObservability( builder => builder.EnableOnObservablePropertyChangedMethod =
                                            false );
    }
}