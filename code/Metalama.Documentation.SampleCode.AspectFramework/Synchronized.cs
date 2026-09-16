// This is public domain Metalama sample code.

namespace Doc.Synchronized;

[Synchronized]
internal class SynchronizedClass
{
    private double _total;
    private int _samplesCount;

    public void AddSample( double sample )
    {
        this._samplesCount++;
        this._total += sample;
    }

    public void Reset()
    {
        this._total = 0;
        this._samplesCount = 0;
    }

    public double Average => this._samplesCount / this._total;
}