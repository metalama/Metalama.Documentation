// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0067.Fixed;

internal class Counter
{
    static Counter()
    {
        StartTime = DateTime.Now;
    }

    public static DateTime StartTime { get; }

    public int Value { get; set; }

    // Fixed: the aspect invokes the parameterless instance constructor.
    [FactoryMethod]
    public static Counter Create() => throw new NotImplementedException();
}
