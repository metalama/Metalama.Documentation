// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.LAMA5163.Error;

[Observable]
public partial class Customer
{
    public bool ShipToBillingAddress { get; set; } = true;

    public Address BillingAddress { get; set; } = new();

    public Address ShippingAddress { get; set; } = new();

    public Address DeliveryAddress => this.ShipToBillingAddress ? this.BillingAddress : this.ShippingAddress;

    // Warning: DeliveryAddress isn't an auto-property, so changes to its City can't be observed.
    public string DeliveryCity => this.DeliveryAddress.City;
}
