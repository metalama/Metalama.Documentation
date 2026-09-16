// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.NotEmptyContract;

public class Instrument
{
    // Neither null nor empty strings are allowed.
    [NotNull]
    [NotEmpty]
    public string Name { get; set; }

    // Null strings are allowed but not empty strings.
    [NotEmpty]
    public string? Description { get; set; }

    // Equivalent to [NotNull, NotEmpty]
    [Required]
    public string Currency { get; set; }

    public Instrument( string name, string currency, string? description )
    {
        this.Name = name;
        this.Description = description;
        this.Currency = currency;
    }
}