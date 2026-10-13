namespace Doc.SwitchPerModule.Billing
{
  // This module is migrated to the new calculator.
  public class InvoiceService
  {
    private readonly LegacyTaxCalculator _calculator = new();
    public decimal ComputeTotal(decimal amount, string region) => amount + NewTaxCalculator.ComputeTax(this._calculator, amount, region);
  }
}
namespace Doc.SwitchPerModule.Reporting
{
  // This module still uses the legacy calculator.
  public class TaxReport
  {
    private readonly LegacyTaxCalculator _calculator = new();
    public decimal ComputeTax(decimal revenue, string region) => this._calculator.ComputeTax(revenue, region);
  }
}