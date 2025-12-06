// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Observability.Configuration;
using System;

namespace Doc.Constant_Fabric;

[Observable]
public class Vector
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Norm => VectorHelper.ComputeNorm( this );

    public Vector Direction => VectorHelper.Normalize( this );
}

public static class VectorHelper
{
    public static double ComputeNorm( Vector v ) => Math.Sqrt( (v.X * v.X) + (v.Y * v.Y) );

    public static Vector Normalize( Vector v )
    {
        var norm = ComputeNorm( v );

        return new Vector { X = v.X / norm, Y = v.Y / norm };
    }
}

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectReflectionType( typeof(VectorHelper) )
            .ConfigureObservability( builder => builder.ObservabilityContract =
                                         ObservabilityContract.Constant );
    }
}