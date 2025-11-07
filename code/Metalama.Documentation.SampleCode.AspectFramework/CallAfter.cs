// This is public domain Metalama sample code.

using System;

namespace Doc.CallAfter;

public class TestClass
{
    private int _state;

    [CallAfter( nameof(CheckState) )]
    public void Open()
    {
        this._state++;
    }

    [CallAfter( nameof(CheckState) )]
    public void Close()
    {
        this._state--;
    }

    private void CheckState()
    {
        if ( this._state < 0 )
        {
            throw new InvalidOperationException();
        }
    }
}