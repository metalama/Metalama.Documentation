namespace Doc.LAMA0257.Fixed;
// Fixed: the attribute sets CategoryName, a regular property of the aspect.
[Trackable(CategoryName = "Orders")]
internal partial class Order
{
  public int Id { get; set; }
  public string Category
  {
    get
    {
      return "Orders";
    }
  }
}