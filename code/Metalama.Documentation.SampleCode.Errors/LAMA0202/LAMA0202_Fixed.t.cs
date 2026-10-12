namespace Doc.LAMA0202.Fixed;
[MultiplicationTable]
public partial class Calculator
{
  public object GetMultiplicationTable()
  {
    return new int[][]
    {
      new int[]
      {
        1,
        2,
        3
      },
      new int[]
      {
        2,
        4,
        6
      },
      new int[]
      {
        3,
        6,
        9
      }
    };
  }
}