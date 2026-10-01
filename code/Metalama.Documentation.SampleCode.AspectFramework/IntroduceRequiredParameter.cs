// This is public domain Metalama sample code.

namespace Doc.IntroduceRequiredParameter;

[AddTimestamp]
internal class Order
{
    public Order( int id )
    {
        this.Id = id;
    }

    public Order( int id, string label ) : this( id )
    {
        this.Label = label;
    }

    public int Id { get; }

    public string? Label { get; }
}
