// This is public domain Metalama sample code.

namespace Doc.GenericSameShape;

public record Order( int Id );

public record Customer( string Name );

// The methods have the same shape: one parameter of a different type.
public static class Repository
{
    public static void SaveOrder( Order order ) { }

    public static void SaveCustomer( Customer customer ) { }
}

[CheckRepositoryArguments]
public class ImportService
{
    public void Import( Order order, Customer customer )
    {
        Repository.SaveCustomer( customer );
        Repository.SaveOrder( order );
    }
}
