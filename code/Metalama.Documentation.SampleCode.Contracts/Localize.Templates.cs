// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;
using System;

// ReSharper disable StringLiteralTypo

namespace Doc.Localize;

internal class FrenchTemplates : ContractTemplates
{
    public override void OnPhoneContractViolated( dynamic? value, ContractContext context )
    {
        if ( context.Direction == ContractDirection.Input )
        {
            throw new ArgumentException(
                "La valeur doit être un numéro de téléphone correct.",
                context.TargetParameterName );
        }
        else
        {
            throw new PostconditionViolationException(
                "La valeur doit être un numéro de téléphone correct." );
        }
    }
}