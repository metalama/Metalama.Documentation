// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.NotNull;

internal class NotNullAttribute : ContractAspect
{
    public override void Validate( dynamic? value )
    {
        if ( value == null )
        {
            if ( meta.Target.ContractDirection == ContractDirection.Input )
            {
                throw new ArgumentNullException( nameof(value) );
            }
            else
            {
                throw new PostConditionFailedException(
                    $"'{nameof(value)}' cannot be null when the method returns." );
            }
        }
    }
}