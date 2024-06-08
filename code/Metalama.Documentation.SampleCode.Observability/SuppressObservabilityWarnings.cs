﻿// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System;

namespace Doc.SuppressObservabilityWarnings;

[Observable]
public class Vector
{
    public double X { get; set; }

    public double Y { get; set; }
    
    [SuppressObservabilityWarnings]
    public double Norm => VectorHelper.ComputeNorm( this );
}

public static class VectorHelper
{
    public static double ComputeNorm( Vector v ) => Math.Sqrt( v.X * v.X + v.Y * v.Y );
}