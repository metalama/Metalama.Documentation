// This is public domain Metalama sample code.

namespace Doc.LAMA0901.Error;

// Warning: IPaymentMethod can only be implemented in the project that defines it.
internal class GiftCardPayment : IPaymentMethod
{
    public string Name => "Gift card";
}

internal class Checkout
{
    public IPaymentMethod GetPaymentMethod() => new GiftCardPayment();
}
