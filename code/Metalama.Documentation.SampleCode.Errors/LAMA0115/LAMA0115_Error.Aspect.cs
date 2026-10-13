// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0115.Error;

public class LogReadAttribute : OverrideFieldOrPropertyAspect
{
    public override dynamic? OverrideProperty
    {
        get
        {
            // meta.Target.Field is available only when the target is a field.
            Console.WriteLine( $"Reading {meta.Target.Field.Name}." );

            return meta.Proceed();
        }

        set
        {
            meta.Proceed();
        }
    }
}
