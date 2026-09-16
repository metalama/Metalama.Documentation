using System;
using System.Security;
using System.Security.Principal;
namespace Doc.Authorize;
public class Program
{
  [Authorize]
  public static void SaveIdentityDetails()
  {
    var currentUser = WindowsIdentity.GetCurrent().Name;
    if (currentUser == "MrAllmighty")
    {
      // A sensitive method that should
      // ideally only be called by the current user.
      Console.WriteLine("Saving identity details...");
      return;
    }
    else
    {
      throw new SecurityException("This method can only be called by MrAllmighty.");
    }
  }
  public static void Main()
  {
    try
    {
      SaveIdentityDetails();
    }
    catch (Exception e)
    {
      Console.Error.WriteLine(e.ToString());
    }
  }
}