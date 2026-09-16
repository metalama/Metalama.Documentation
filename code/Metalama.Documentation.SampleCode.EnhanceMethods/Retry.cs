// This is public domain Metalama sample code.

using System;
using System.Net;

namespace Doc.RetryFew;

public class Exchange
{
    [Retry]
    public double GetExchangeRate()
    {
        // Simulates a call to an exchange rate web API.
        // Sometimes the connection may fail.

        var n = new Random( 5 ).Next( 20 );

        if ( n % 2 == 0 )
        {
            return 0.5345;
        }
        else
        {
            throw new WebException( "The service is not available." );
        }
    }
}

public class Program
{
    public static void Main()
    {
        var x = new Exchange();
        var rate = x.GetExchangeRate();
        Console.WriteLine( rate );
    }
}