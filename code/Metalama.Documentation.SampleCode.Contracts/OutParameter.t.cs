using Metalama.Patterns.Contracts;
namespace Doc.Contracts.OutParameter;
public interface ICustomerService
{
  // Returns the name of a given customer or null if it cannot be found,
  // but never returns an empty string.
  bool TryGetCustomerName(int id, [NotEmpty] out string? name);
}
public class CustomerService : ICustomerService
{
  public bool TryGetCustomerName(int id, out string? name)
  {
    bool returnValue;
    if (id == 1)
    {
      name = "Orontes I the Bactrian";
      returnValue = true;
    }
    else
    {
      name = null;
      returnValue = false;
    }
    if (name != null && name.Length <= 0)
    {
      throw new PostconditionViolationException("The 'name' parameter must not be null or empty.");
    }
    return returnValue;
  }
}