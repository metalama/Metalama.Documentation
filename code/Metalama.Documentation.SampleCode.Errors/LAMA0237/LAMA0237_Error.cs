// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0237.Error;

public interface IClock
{
    DateTime Now { get; }
}

public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}

// The abstract ClockAttribute aspect can't be applied until a derived aspect implements the Clock property.
internal class Invoice { }
