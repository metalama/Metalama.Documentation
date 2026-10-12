// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5165.Fixed;

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
            // Fixed: City is accessed through the properties, without a local variable.
            return this.ShipToBillingAddress ? this.BillingAddress.City : this.ShippingAddress.City;
        }
    }
}
