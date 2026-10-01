using System;
namespace Doc.ToStringWithSimpleToString;
[ToString]
internal class MovingVertex
{
  public double X;
  public double Y;
  public double DX;
  public double DY { get; set; }
  public double Velocity => Math.Sqrt((this.DX * this.DX) + (this.DY * this.DY));
  public override string ToString()
  {
    return $"{{ MovingVertex X={X}, Y={Y}, DX={DX}, DY={DY}, Velocity={Velocity} }}";
  }
}