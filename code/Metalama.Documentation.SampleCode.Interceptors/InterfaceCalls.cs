// This is public domain Metalama sample code.

namespace Doc.InterfaceCalls;

public record Order( int Id );

public interface IOrderRepository
{
    void Save( Order order );
}

public class SqlOrderRepository : IOrderRepository
{
    public void Save( Order order ) { }
}

[LogRepositoryCalls]
public class OrderService
{
    private readonly IOrderRepository _repository = new SqlOrderRepository();
    private readonly SqlOrderRepository _sqlRepository = new();

    // The call is bound to the interface method.
    public void Place( Order order ) => this._repository.Save( order );

    // The call is bound to the implementation.
    public void Import( Order order ) => this._sqlRepository.Save( order );
}
