// This is public domain Metalama sample code.

namespace Doc.LAMA0108.Error;

internal class CustomerService
{
    [LogNullArguments]
    public void Register( string? name, string? email ) { }
}
