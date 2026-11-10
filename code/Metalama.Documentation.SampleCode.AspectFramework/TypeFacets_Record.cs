// This is public domain Metalama sample code.

namespace Doc.TypeFacets_Record;

[ToTuple]
public partial record Product( string Code, string Name, decimal Price )
{
    public string? Description { get; init; }
}
