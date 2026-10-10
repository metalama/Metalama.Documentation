// This is public domain Metalama sample code.

namespace Doc.LAMA0256.Error;

internal class Customer
{
    public string? Name { get; set; }

    public string? Email { get; set; }
}

internal class CustomerService
{
    [LogProperties]
    public void Save( Customer customer ) { }
}
