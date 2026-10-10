// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Predicates;
using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0907.Fixed;

// This class derives from ReferencePredicate and has a constructor accepting a ReferencePredicateBuilder.
[CompileTime]
internal class ExcludeTestTypesPredicate : ReferencePredicate
{
    public ExcludeTestTypesPredicate( ReferencePredicateBuilder builder ) : base( builder ) { }

    protected override bool IsMatchCore( ReferenceValidationContext context ) => context.Origin.Type.Name.EndsWith( "Tests" );

    protected override ReferenceGranularity GetGranularity() => ReferenceGranularity.Type;
}
