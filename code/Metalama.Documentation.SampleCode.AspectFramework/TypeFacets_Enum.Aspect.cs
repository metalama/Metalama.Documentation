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
        builder.Type().MustSatisfy( t => t is INamedType { Facets.Enum: not null }, t => $"{t} must be an enum" );
    }

    public override void BuildEligibility( IEligibilityBuilder<IFieldOrPropertyOrIndexer> builder )
    {
        base.BuildEligibility( builder );
        builder.Type().MustSatisfy( t => t is INamedType { Facets.Enum: not null }, t => $"{t} must be an enum" );
    }

    public override void Validate( dynamic? value )
    {
        // Read the enum facet of the target type.
        var enumType = (INamedType) ((IHasType) meta.Target.Declaration).Type;
        var isInvalid = BuildIsInvalidExpression( (IExpression) value!, enumType.Facets.Enum! );

        if ( isInvalid.Value )
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                $"The value is not defined in the '{enumType.Name}' enum." );
        }
    }

    // Builds an expression that is true when the value is not defined in the enum.
    private static IExpression BuildIsInvalidExpression( IExpression value, IEnumFacet enumFacet )
    {
        var expression = new ExpressionBuilder();

        if ( enumFacet.IsFlags )
        {
            // For a [Flags] enum, any combination of the declared bits is valid.
            long allFlags = 0;

            foreach ( var member in enumFacet.Members )
            {
                allFlags |= Convert.ToInt64( member.ConstantValue!.Value.Value );
            }

            // Generates `((long) value & ~allFlags) != 0`.
            expression.AppendVerbatim( "((long) " );
            expression.AppendExpression( value );
            expression.AppendVerbatim( " & ~" );
            expression.AppendLiteral( allFlags, stronglyTyped: true );
            expression.AppendVerbatim( ") != 0" );
        }
        else
        {
            // Otherwise, the value must be one of the declared members.
            // Generates `value is not (Member1 or Member2 or ...)`.
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
        }

        return expression.ToExpression();
    }
}
