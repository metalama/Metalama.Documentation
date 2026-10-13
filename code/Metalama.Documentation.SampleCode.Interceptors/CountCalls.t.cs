namespace Doc.CountCalls;
public class InvoiceService
{
  private readonly LegacyTaxCalculator _calculator = new();
  public decimal ComputeTotal(decimal amount, string region) => amount + CallCounter.ComputeTax(this._calculator, amount, region, caller: "ComputeTotal", line: 10);
  public decimal ComputeRefund(decimal amount, string region) => -(amount + CallCounter.ComputeTax(this._calculator, amount, region, caller: "ComputeRefund", line: 13));
}
public class LegacyTaxCalculator
{
  public decimal ComputeTax(decimal amount, string region) => region == "EU" ? amount * 0.2m : amount * 0.1m;
}