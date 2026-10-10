// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0901.Fixed;

// Only the project that defines this interface can implement it.
[InternalOnlyImplement]
public interface IPaymentMethod
{
    string Name { get; }
}

public class CreditCardPayment : IPaymentMethod
{
    public string Name => "Credit card";
}

// Fixed: the implementation is moved to the project that defines the interface.
public class GiftCardPayment : IPaymentMethod
{
    public string Name => "Gift card";
}
