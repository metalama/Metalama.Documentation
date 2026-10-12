namespace Doc.LAMA0060.Fixed;
[Counter]
internal partial class PageVisits
{
  private int _count;
  public int Count
  {
    get
    {
      return _count;
    }
  }
  public void Increment()
  {
    _count++;
  }
}