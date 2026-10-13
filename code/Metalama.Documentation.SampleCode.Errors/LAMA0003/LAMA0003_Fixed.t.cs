namespace Doc.LAMA0003.Fixed;
// Fixed: the type aspect is applied to the type.
[Describe]
internal partial class Invoice
{
  public decimal Amount { get; set; }
  public decimal GetTotal(decimal taxRate) => this.Amount * (1 + taxRate);
  public string Describe()
  {
    return "Invoice";
  }
}