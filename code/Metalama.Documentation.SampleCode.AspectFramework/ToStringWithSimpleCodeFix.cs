// This is public domain Metalama sample code.

using System;

namespace Doc.ToStringWithSimpleToString;

[ToString]
internal class MovingVertex
{
    public double X;

    public double Y;

    public double DX;

    public double DY { get; set; }

    public double Velocity => Math.Sqrt( (this.DX * this.DX) + (this.DY * this.DY) );
}