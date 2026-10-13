// This is public domain Metalama sample code.

namespace Doc.CompareTaxCalculators;

public class InvoiceService
{
    private readonly LegacyTaxCalculator _calculator = new();

    public decimal ComputeTotal( decimal amount, string region )
        => amount + this._calculator.ComputeTax( amount, region );
}
