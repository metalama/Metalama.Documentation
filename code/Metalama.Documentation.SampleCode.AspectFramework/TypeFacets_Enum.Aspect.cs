// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Code.Types;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.TypeFacets_Enum;

// Throws when a parameter or property receives a value that the enum does not declare.
public class DefinedEnumValueAttribute : ContractAspect
{
    public override void BuildEligibility( IEligibilityBuilder<IParameter> builder )
    {
        base.BuildEligibility( builder );
        builder.Type().MustSatisfy( IsNonFlagsEnum, t => $"{t} must be an enum without the [Flags] attribute" );
    }

    public override void BuildEligibility( IEligibilityBuilder<IFieldOrPropertyOrIndexer> builder )
    {
        base.BuildEligibility( builder );
        builder.Type().MustSatisfy( IsNonFlagsEnum, t => $"{t} must be an enum without the [Flags] attribute" );
    }

    // Reads the enum facet: it is null when the type is not an enum.
    private static bool IsNonFlagsEnum( IType type ) => type is INamedType { Facets.Enum.IsFlags: false };

    public override void Validate( dynamic? value )
    {
        var enumType = (INamedType) ((IHasType) meta.Target.Declaration).Type;
        var isUndefined = BuildIsUndefinedExpression( (IExpression) value!, enumType.Facets.Enum! );

        if ( isUndefined.Value )
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                $"The value is not defined in the '{enumType.Name}' enum." );
        }
    }

    // Builds the expression `value is not (Member1 or Member2 or ...)`.
    private static IExpression BuildIsUndefinedExpression( IExpression value, IEnumFacet enumFacet )
    {
        var expression = new ExpressionBuilder();
        expression.AppendExpression( value );
        expression.AppendVerbatim( " is not (" );

        for ( var i = 0; i < enumFacet.Members.Count; i++ )
        {
            if ( i > 0 )
            {
                expression.AppendVerbatim( " or " );
            }

            expression.AppendExpression( enumFacet.Members[i] );
        }

        expression.AppendVerbatim( ")" );

        return expression.ToExpression();
    }
}
