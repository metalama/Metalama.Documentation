// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using System;

namespace Doc.ExpressionBuilder_;

public class NotInAttribute : ContractAspect
{
    private readonly int[] _forbiddenValues;

    public NotInAttribute( params int[] forbiddenValues )
    {
        this._forbiddenValues = forbiddenValues;
    }

    public override void Validate( dynamic? value )
    {
        // Build the expression.
        var expressionBuilder = new ExpressionBuilder();
        expressionBuilder.AppendExpression( value );
        expressionBuilder.AppendVerbatim( " is ( " );

        var requiresOr = meta.CompileTime( false );

        foreach ( var forbiddenValue in this._forbiddenValues )
        {
            if ( requiresOr )
            {
                expressionBuilder.AppendVerbatim( " or " );
            }
            else
            {
                requiresOr = true;
            }

            expressionBuilder.AppendLiteral( forbiddenValue );
        }

        expressionBuilder.AppendVerbatim( ")" );
        var condition = expressionBuilder.ToExpression();

        // Use the expression in run-time code.
        if ( condition.Value )
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}