// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The program does not run until metalama/Metalama#2235 is fixed:
// [ExcludeInterceptors] has no effect.
// @DisableExecuteProgram
#endif

namespace Doc.CompareTaxCalculators;

public class InvoiceService
{
    private readonly LegacyTaxCalculator _calculator = new();

    public decimal ComputeTotal( decimal amount, string region )
        => amount + this._calculator.ComputeTax( amount, region );
}
