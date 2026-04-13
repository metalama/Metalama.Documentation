// This is public domain Metalama sample code.

using System;

namespace Doc.CreateDelegateExpression;

public class TestClass
{
    [RegisterCallback( nameof(OnCompleted) )]
    public void DoWork()
    {
        Console.WriteLine( "Doing work..." );
    }

    private void OnCompleted( string message )
    {
        Console.WriteLine( $"Callback: {message}" );
    }
}
