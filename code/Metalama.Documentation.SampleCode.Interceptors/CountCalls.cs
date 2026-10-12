// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The program does not run until metalama/Metalama#2235 is fixed: [ExcludeInterceptors] has no effect.
// @DisableExecuteProgram
#endif

namespace Doc.CountCalls;

public class InvoiceService
{
    private readonly LegacyTaxCalculator _calculator = new();

    public decimal ComputeTotal( decimal amount, string region )
        => amount + this._calculator.ComputeTax( amount, region );

    public decimal ComputeRefund( decimal amount, string region )
        => -(amount + this._calculator.ComputeTax( amount, region ));
}

public class LegacyTaxCalculator
{
    public decimal ComputeTax( decimal amount, string region )
        => region == "EU" ? amount * 0.2m : amount * 0.1m;
}
