// This is public domain Metalama sample code.

namespace Doc.AfterObjectInitializer;

[ValidateAfterInitialization]
public partial class Invoice
{
    public required string Number { get; init; }

    public required decimal Amount { get; init; }
}
