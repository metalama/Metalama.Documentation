using System;
namespace Doc.LAMA0043.Fixed;
[Log]
internal class AccountService
{
  private decimal _balance = 100;
  public void Withdraw(decimal amount)
  {
    Console.WriteLine("Executing AccountService.Withdraw(decimal).");
    this._balance -= amount;
  }
  // The code fix added [NotLogged], so the aspect no longer logs this method or suggests the fix here.
  [NotLogged]
  public decimal GetBalance() => this._balance;
}