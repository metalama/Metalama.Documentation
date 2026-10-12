// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0655.Error;

[LogEquals]
public record Sensor( string Name )
{
    // The accessor templates of the aspect do not call meta.Proceed(), so the event has no backing field.
    public event EventHandler? Changed;
}
