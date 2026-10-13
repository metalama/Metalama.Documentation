namespace Doc.CompareTaxCalculators;
public class InvoiceService
{
  private readonly LegacyTaxCalculator _calculator = new();
  public decimal ComputeTotal(decimal amount, string region) => amount + TaxComparison.ComputeTax(this._calculator, amount, region);
}