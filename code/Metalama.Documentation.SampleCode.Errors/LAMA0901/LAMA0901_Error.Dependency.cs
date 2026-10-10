// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0901.Error;

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
