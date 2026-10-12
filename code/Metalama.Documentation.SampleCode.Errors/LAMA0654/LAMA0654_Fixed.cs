// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0654.Fixed;

[LogEquals]
public record Product( string Name )
{
    // The getter is automatic, so reading the property is the same as reading the backing field.
    public double Price
    {
        get;
        set => field = Math.Round( value, 2 );
    }
}
