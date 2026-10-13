namespace Doc.LAMA0555.Fixed;
[GenerateResult]
public partial class OrderService
{
  public union OrderResult(int, string)
  {
    public string Label
    {
      get
      {
        return "Order result";
      }
    }
  }
}