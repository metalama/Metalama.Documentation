using System;
namespace Doc.CompileTimeRules_OwnType;
public class AccountService
{
  public void CreateAccount(string userName, string password)
  {
    Console.WriteLine($"[Accounts] Executing CreateAccount(userName: {userName}, password: ***).");
  }
}