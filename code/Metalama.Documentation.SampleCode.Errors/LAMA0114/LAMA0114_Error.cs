// This is public domain Metalama sample code.

namespace Doc.LAMA0114.Error;

internal class Calculator
{
    private int _total;

    [Trace]
    public int Add( int value ) => this._total += value;

    // Error: the template of [Trace] uses meta.This, but this method is static.
    [Trace]
    public static int Multiply( int a, int b ) => a * b;
}
