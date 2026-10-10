// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0067.Error;

internal class Counter
{
    static Counter()
    {
        StartTime = DateTime.Now;
    }

    public static DateTime StartTime { get; }

    public int Value { get; set; }

    // Error: the aspect tries to invoke the static constructor.
    [FactoryMethod]
    public static Counter Create() => throw new NotImplementedException();
}
