using Metalama.Patterns.Contracts;
namespace Doc.Contracts.ReturnValue;
public interface ICustomerService
{
  // Returns the name of a given customer or null if it cannot be found,
  // but never returns an empty string.
  [return: NotEmpty]
  string? GetCustomerName(int id);
}
public class CustomerService : ICustomerService
{
  public string? GetCustomerName(int id)
  {
    string? returnValue;
    if (id == 1)
    {
      returnValue = "Orontes I the Bactrian";
    }
    else
    {
      returnValue = null;
    }
    if (returnValue != null && returnValue.Length <= 0)
    {
      throw new PostconditionViolationException("The return value must not be null or empty.");
    }
    return returnValue;
  }
}