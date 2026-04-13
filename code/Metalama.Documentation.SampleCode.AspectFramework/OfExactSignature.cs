// This is public domain Metalama sample code.

using System;

namespace Doc.OfExactSignature;

public class Calculator
{
    [WrapWithValidation]
    public int Divide( int numerator, int denominator )
    {
        return numerator / denominator;
    }

    private void Validate( int numerator, int denominator )
    {
        if ( denominator == 0 )
        {
            throw new ArgumentException( "Denominator cannot be zero.", nameof(denominator) );
        }
    }
}
