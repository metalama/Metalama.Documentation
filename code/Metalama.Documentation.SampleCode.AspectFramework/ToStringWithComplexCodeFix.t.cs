using System;
namespace Doc.ToStringWithComplexCodeFix;
[ToString]
internal class MovingVertex
{
  public double X;
  public double Y;
  public double DX;
  public double DY { get; set; }
  [NotToString]
  public double Velocity => Math.Sqrt((this.DX * this.DX) + (this.DY * this.DY));
  public override string ToString()
  {
    return $"{{ MovingVertex X={X}, Y={Y}, DX={DX}, DY={DY} }}";
  }
}