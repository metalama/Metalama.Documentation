// This is public domain Metalama sample code.

namespace Doc.LAMA0019.Error;

internal class RetryPolicy
{
    private int _maxAttempts;

    public int MaxAttempts
    {
        get => this._maxAttempts;
        set => this._maxAttempts = value;
    }

    // Error: a property can't be passed to the 'out' parameter of int.TryParse.
    [ParseInto( nameof(MaxAttempts) )]
    public void Configure( string text ) { }
}
