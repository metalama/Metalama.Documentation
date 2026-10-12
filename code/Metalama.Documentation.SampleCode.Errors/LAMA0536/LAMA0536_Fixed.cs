// This is public domain Metalama sample code.

namespace Doc.LAMA0536.Fixed;

// The [AddTimestamp] aspect adds a creationTime parameter and a forwarding constructor.
[AddTimestamp]
internal class Order
{
    public Order( int id )
    {
        this.Id = id;
    }

    public int Id { get; }
}
