// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using System;

namespace Doc.CompareTaxCalculators;

[ExcludeInterceptors]
public static class TaxComparison
{
    public static decimal ComputeTax( LegacyTaxCalculator calculator, decimal amount, string region )
    {
        var legacyResult = calculator.ComputeTax( amount, region );

        try
        {
            var newResult = NewTaxCalculator.ComputeTax( amount, region );

            if ( newResult != legacyResult )
            {
                Console.WriteLine( $"Mismatch for {amount} in {region}: legacy {legacyResult}, new {newResult}." );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"The new calculator failed for {amount} in {region}: {e.Message}" );
        }

        return legacyResult;
    }
}
