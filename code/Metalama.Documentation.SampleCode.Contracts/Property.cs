// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;

namespace Doc.Contracts.Property;

public interface IItem
{
    [NotEmpty]
    string Key { get; }

    [NotEmpty( Direction = ContractDirection.Both )]
    string Value { get; set; }
}

public class Item : IItem
{
    public string Key { get; }

    public string Value { get; set; }

    public Item( string key, string value )
    {
        this.Key = key;
        this.Value = value;
    }
}