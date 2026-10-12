namespace Doc.CountCalls;
public class InvoiceService
{
  private readonly LegacyTaxCalculator _calculator = new();
  public decimal ComputeTotal(decimal amount, string region) => amount + CallCounter.ComputeTax(this._calculator, amount, region, caller: "ComputeTotal", line: 11);
  public decimal ComputeRefund(decimal amount, string region) => -(amount + CallCounter.ComputeTax(this._calculator, amount, region, caller: "ComputeRefund", line: 14));
}
public class LegacyTaxCalculator
{
  public decimal ComputeTax(decimal amount, string region) => region == "EU" ? amount * 0.2m : amount * 0.1m;
}