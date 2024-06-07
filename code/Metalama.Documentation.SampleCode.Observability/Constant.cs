﻿// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System;

namespace Doc.Constant;

[Observable]
public class Vector
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Norm => VectorHelper.ComputeNorm( this );
}

public static class VectorHelper
{
    [Constant]
    public static double ComputeNorm( Vector v ) => Math.Sqrt( v.X * v.X + v.Y * v.Y );
}