// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0655.Fixed;

[LogEquals]
public record Sensor( string Name )
{
    // The accessor templates of the aspect call meta.Proceed(), so the event keeps its backing field.
    public event EventHandler? Changed;
}
