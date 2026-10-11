// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0654.Error;

[LogEquals]
public record Product( string Name )
{
    // The getter has a body, so the original Equals reads the backing field but the overridden one reads the property.
    public double Price
    {
        get => Math.Round( field, 2 );
        set => field = value;
    }
}
