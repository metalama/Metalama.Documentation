// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.RequiredContract;

public class Instrument
{
    [Required]
    public string Name { get; set; }

    [Required]
    public Category Category { get; set; }

    public Instrument( [Required] string name, [Required] Category category )
    {
        this.Name = name;
        this.Category = category;
    }
}

public class Category { }