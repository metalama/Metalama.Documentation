// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5163.Fixed;

[Observable]
public partial class Customer
{
    public bool ShipToBillingAddress { get; set; } = true;

    public Address BillingAddress { get; set; } = new();

    public Address ShippingAddress { get; set; } = new();

    public Address DeliveryAddress => this.ShipToBillingAddress ? this.BillingAddress : this.ShippingAddress;

    // Fixed: City is accessed through the auto-properties.
    public string DeliveryCity => this.ShipToBillingAddress ? this.BillingAddress.City : this.ShippingAddress.City;
}
