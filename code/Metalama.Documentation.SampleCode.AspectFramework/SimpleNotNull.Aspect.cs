// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.SimpleNotNull;

public class NotNullAttribute : ContractAspect
{
    public override void Validate( dynamic? value )
    {
        if ( value == null! )
        {
            throw new ArgumentNullException( nameof(value) );
        }
    }
}