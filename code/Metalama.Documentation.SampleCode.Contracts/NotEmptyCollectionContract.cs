// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;
using System.Collections.Generic;

namespace Doc.NotEmptyCollectionContract;

public class Submenu
{
    public string Name { get; }

    public IReadOnlyCollection<MenuItem> Items { get; }

    public Submenu(
        [Required] string name,
        [NotNull] [NotEmpty] IReadOnlyCollection<MenuItem> items )
    {
        this.Name = name;
        this.Items = items;
    }
}

public record MenuItem( string Name );