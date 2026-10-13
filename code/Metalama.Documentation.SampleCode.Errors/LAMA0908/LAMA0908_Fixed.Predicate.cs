// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Predicates;
using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0908.Fixed;

// The constructor accepts only a ReferencePredicateBuilder.
[CompileTime]
internal class ExcludeTestTypesPredicate : ReferencePredicate
{
    public ExcludeTestTypesPredicate( ReferencePredicateBuilder builder ) : base( builder ) { }

    protected override bool IsMatchCore( ReferenceValidationContext context ) => context.Origin.Type.Name.EndsWith( "Tests" );

    protected override ReferenceGranularity GetGranularity() => ReferenceGranularity.Type;
}
