namespace Doc.InterfaceCalls;
public record Order(int Id);
public interface IOrderRepository
{
  void Save(Order order);
}
public class SqlOrderRepository : IOrderRepository
{
  public void Save(Order order)
  {
  }
}
[LogRepositoryCalls]
public class OrderService
{
  private readonly IOrderRepository _repository = new SqlOrderRepository();
  private readonly SqlOrderRepository _sqlRepository = new();
  // The call is bound to the interface method.
  public void Place(Order order) => MetalamaInterceptors.IOrderRepository_Save_Interceptor(this._repository, order: order);
  // The call is bound to the implementation.
  public void Import(Order order) => MetalamaInterceptors.SqlOrderRepository_Save_Interceptor(this._sqlRepository, order: order);
}