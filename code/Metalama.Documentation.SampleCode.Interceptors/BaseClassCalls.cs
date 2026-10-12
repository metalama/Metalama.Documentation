// This is public domain Metalama sample code.

namespace Doc.BaseClassCalls;

public class OrderGrid : Control
{
    private int _rowCount;

    public void AddRow()
    {
        this._rowCount++;
        this.Invalidate();
    }

    public void Activate() => this.Focus();
}

internal class Program
{
    private static void Main()
    {
        var grid = new OrderGrid();
        grid.AddRow();
        grid.Activate();
    }
}
