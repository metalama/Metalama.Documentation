using System;
namespace Doc.BaseClassCalls;
// The base class of all the controls of the application.
public abstract class AppControl : Control
{
  private protected static void Control_UpdateStyles_Interceptor(AppControl receiver)
  {
    Console.WriteLine("Updating styles.");
    receiver.UpdateStyles();
  }
}
public class OrderGrid : AppControl
{
  public void ApplyTheme() => Control_UpdateStyles_Interceptor(this);
}
public class CustomerGrid : AppControl
{
  public void ApplyTheme() => Control_UpdateStyles_Interceptor(this);
}
internal class Program
{
  private static void Main()
  {
    new OrderGrid().ApplyTheme();
    new CustomerGrid().ApplyTheme();
  }
}