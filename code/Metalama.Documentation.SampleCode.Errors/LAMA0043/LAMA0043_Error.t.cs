// Hidden CS8019 on `using Metalama.Extensions.CodeFixes;`: `Unnecessary using directive.`
// Hidden CS8019 on `using System.Linq;`: `Unnecessary using directive.`
// Hidden LAMA0043 on `Withdraw`: `Code fix`
//    CodeFix: Exclude from logging`
// Hidden LAMA0043 on `GetBalance`: `Code fix`
//    CodeFix: Exclude from logging`
using System;
namespace Doc.LAMA0043.Error;
// The [Log] aspect suggests a code fix for each method, carried by LAMA0043.
[Log]
internal class AccountService
{
  private decimal _balance = 100;
  public void Withdraw(decimal amount)
  {
    Console.WriteLine("Executing AccountService.Withdraw(decimal).");
    this._balance -= amount;
  }
  public decimal GetBalance()
  {
    Console.WriteLine("Executing AccountService.GetBalance().");
    return this._balance;
  }
}