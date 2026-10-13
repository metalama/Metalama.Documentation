using System;
namespace Doc.BaseClassCalls;
// The base class of all the controls of the application.
public abstract class AppControl : Control
{
  private protected void Control_UpdateStyles_Interceptor()
  {
    Console.WriteLine("Updating styles.");
    this.UpdateStyles();
  }
}
public class OrderGrid : AppControl
{
  public void ApplyTheme() => this.Control_UpdateStyles_Interceptor();
}
public class CustomerGrid : AppControl
{
  public void ApplyTheme() => this.Control_UpdateStyles_Interceptor();
}
internal class Program
{
  private static void Main()
  {
    new OrderGrid().ApplyTheme();
    new CustomerGrid().ApplyTheme();
  }
}