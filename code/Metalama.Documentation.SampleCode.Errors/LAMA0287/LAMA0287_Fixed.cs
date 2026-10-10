// This is public domain Metalama sample code.

namespace Doc.LAMA0287.Fixed;

internal class Calculator
{
    private int _total;

    [Trace]
    public int Add( int value ) => this._total += value;

    // Fixed: the template of [Trace] no longer uses meta.Receiver on methods without a receiver.
    [Trace]
    public static int Multiply( int a, int b ) => a * b;
}
