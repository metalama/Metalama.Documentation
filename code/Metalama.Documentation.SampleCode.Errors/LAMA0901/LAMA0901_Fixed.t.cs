namespace Doc.LAMA0901.Fixed;
internal class Checkout
{
  // Fixed: the class uses the implementation provided by the project that defines the interface.
  public IPaymentMethod GetPaymentMethod() => new GiftCardPayment();
}