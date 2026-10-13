using System;
namespace Doc.ExpressionBuilder_;
public class Customer
{
  private int _id;
  [NotIn(0, 1, 100)]
  public int Id
  {
    get
    {
      return _id;
    }
    set
    {
      if (value is (0 or 1 or 100))
      {
        throw new ArgumentOutOfRangeException();
      }
      _id = value;
    }
  }
}