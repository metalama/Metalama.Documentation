// This is public domain Metalama sample code.

namespace Doc.LAMA0114.Fixed;

internal class Calculator
{
    private int _total;

    [Trace]
    public int Add( int value ) => this._total += value;

    // Fixed: the template of [Trace] no longer uses meta.This on static methods.
    [Trace]
    public static int Multiply( int a, int b ) => a * b;
}
