// Warning LAMA5005 on `Reset`: `The [SuspendInvariantsAttribute] aspect on method 'Account.Reset(decimal, decimal)' is redundant the type 'Account' does not contain any invariants.`
using Metalama.Patterns.Contracts;
namespace Doc.LAMA5005.Error;
public partial class Account
{
  public decimal Balance { get; set; }
  public decimal CreditLimit { get; set; }
  // Warning: the type has no [Invariant] method, so there is nothing to suspend.
  [SuspendInvariants]
  public void Reset(decimal balance, decimal creditLimit)
  {
    this.CreditLimit = creditLimit;
    this.Balance = balance;
  }
}