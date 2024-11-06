using System;
namespace Doc.LocalVariableTransaction;
public class Account
{
  private decimal _amount;
  public DateTime LastOperation { get; private set; }
  public decimal Amount
  {
    get => this._amount;
    private set
    {
      if (value < 0)
      {
        throw new ArgumentOutOfRangeException();
      }
      this._amount = value;
    }
  }
  [TransactedMethod]
  public void Withdraw(decimal amount)
  {
    var _amount_1 = _amount;
    var LastOperation_1 = LastOperation;
    try
    {
      this.LastOperation = DateTime.Now;
      this.Amount -= amount;
      return;
    }
    catch
    {
      _amount = _amount_1;
      LastOperation = LastOperation_1;
      throw;
    }
  }
}