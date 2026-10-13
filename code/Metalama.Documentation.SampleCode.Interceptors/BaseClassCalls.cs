// This is public domain Metalama sample code.

namespace Doc.BaseClassCalls;

// The base class of all the controls of the application.
public abstract class AppControl : Control;

public class OrderGrid : AppControl
{
    public void ApplyTheme() => this.UpdateStyles();
}

public class CustomerGrid : AppControl
{
    public void ApplyTheme() => this.UpdateStyles();
}

internal class Program
{
    private static void Main()
    {
        new OrderGrid().ApplyTheme();
        new CustomerGrid().ApplyTheme();
    }
}
