// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0237.Fixed;

public interface IClock
{
    DateTime Now { get; }
}

public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}

[Clock]
internal partial class Invoice { }
