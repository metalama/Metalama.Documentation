// This is public domain Metalama sample code.

using System;

namespace Doc.UpdateMethod;

[UpdateMethod]
internal partial class CityHunter
{
    private int _x;

    public string? Y { get; private set; }

    public DateTime Z { get; }
}

internal class Program
{
    private static void Main()
    {
        CityHunter ch = new();
#if METALAMA
            ch.Update(0, "1");
#endif
    }
}