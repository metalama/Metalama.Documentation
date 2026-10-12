// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0115.Fixed;

public class LogReadAttribute : OverrideFieldOrPropertyAspect
{
    public override dynamic? OverrideProperty
    {
        get
        {
            // meta.Target.FieldOrProperty is available for both fields and properties.
            Console.WriteLine( $"Reading {meta.Target.FieldOrProperty.Name}." );

            return meta.Proceed();
        }

        set
        {
            meta.Proceed();
        }
    }
}
