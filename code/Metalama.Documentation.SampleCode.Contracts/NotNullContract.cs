// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.NotNullContract;

public class Instrument
{
    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public Category Category { get; set; }

    public Instrument( [NotNull] string name, [NotNull] Category category )
    {
        this.Name = name;
        this.Category = category;
    }
}

public class Category { }