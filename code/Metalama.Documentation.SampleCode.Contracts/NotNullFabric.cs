// This is public domain Metalama sample code.

namespace Doc.NotNullFabric;

public class Instrument
{
    public string Name { get; set; }

    public Category? Category { get; set; }

    public Instrument( string name, Category? category )
    {
        this.Name = name;
        this.Category = category;
    }
}

public class Category
{
    // Internal APIs won't be checked by default.
    internal Category( string name )
    {
        this.Name = name;
    }

    public string Name { get; }
}