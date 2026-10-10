// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5165.Error;

[Observable]
public partial class Customer
{
    public bool ShipToBillingAddress { get; set; } = true;

    public Address BillingAddress { get; set; } = new();

    public Address ShippingAddress { get; set; } = new();

    public string DeliveryCity
    {
        get
        {
            // Warning: Address is a mutable type, so the local variable can't be observed.
            Address address = this.ShipToBillingAddress ? this.BillingAddress : this.ShippingAddress;

            return address.City;
        }
    }
}
