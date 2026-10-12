// This is public domain Metalama sample code.

namespace Doc.LAMA0280.Error;

internal class CustomerService
{
    [LogNullArguments]
    public void Register( string? name, string? email ) { }
}
