// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System.Collections.Generic;
using System.Linq;

namespace Doc.TypeFacets_Record;

// Introduces a ToTuple method that returns the positional properties of a record as a tuple.
public class ToTupleAttribute : TypeAspect
{
    public override void BuildEligibility( IEligibilityBuilder<INamedType> builder )
    {
        builder.MustSatisfy(
            t => t.Facets.Record is { PositionalProperties.Count: > 0 },
            t => $"{t} must be a positional record" );
    }

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Read the record facet of the target type.
        var properties = builder.Target.Facets.Record!.PositionalProperties;

        // Create a tuple type with one element per positional property.
        var tupleType = TypeFactory.CreateTupleType( properties.Select( p => (p.Type, p.Name) ) );

        builder.IntroduceMethod(
            nameof(this.ToTuple),
            args: new { tupleType, properties },
            buildMethod: m => m.ReturnType = tupleType );
    }

    [Template]
    public dynamic? ToTuple( ITupleType tupleType, IReadOnlyList<IProperty> properties )
    {
        return tupleType.CreateCreateInstanceExpression( properties ).Value;
    }
}
