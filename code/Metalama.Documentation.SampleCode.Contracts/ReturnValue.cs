// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.ReturnValue;

public interface ICustomerService
{
    // Returns the name of a given customer or null if it cannot be found,
    // but never returns an empty string.
    [return: NotEmpty]
    string? GetCustomerName( int id );
}

public class CustomerService : ICustomerService
{
    public string? GetCustomerName( int id )
    {
        if ( id == 1 )
        {
            return "Orontes I the Bactrian";
        }
        else
        {
            return null;
        }
    }
}