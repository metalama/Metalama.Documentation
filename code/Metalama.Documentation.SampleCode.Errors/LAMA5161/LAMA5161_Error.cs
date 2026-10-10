// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5161.Error;

// Person does not implement INotifyPropertyChanged.
public class Person
{
    public string? Name { get; set; }
}

[Observable]
public sealed partial class PersonViewModel
{
    public PersonViewModel( Person person )
    {
        this.Person = person;
    }

    public Person Person { get; set; }

    // Warning: changes of Person.Name cannot be observed.
    public string? DisplayName => this.Person.Name;
}
