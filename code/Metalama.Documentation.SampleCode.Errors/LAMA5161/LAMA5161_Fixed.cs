// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5161.Fixed;

// Fixed: Person implements INotifyPropertyChanged thanks to the [Observable] aspect.
[Observable]
public partial class Person
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

    public string? DisplayName => this.Person.Name;
}
