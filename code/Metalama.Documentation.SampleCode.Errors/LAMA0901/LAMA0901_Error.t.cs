// Warning LAMA0901 on `IPaymentMethod`: `The 'IPaymentMethod' interface can only be implemented by the 'LAMA0901_Error` project or from another project having access to its internals.`
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