namespace Doc.LAMA0262.Fixed;
[Versioned]
internal partial class Invoice
{
  public decimal Amount { get; set; }
  public int SchemaVersion
  {
    get
    {
      return 1;
    }
  }
}