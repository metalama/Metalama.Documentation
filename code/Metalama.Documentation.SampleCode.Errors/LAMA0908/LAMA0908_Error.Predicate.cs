// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Predicates;
using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0908.Error;

[CompileTime]
internal class ExcludeTestTypesPredicate : ReferencePredicate
{
    private readonly string _suffix;

    // The constructor has a second parameter, so Metalama cannot instantiate the predicate.
    public ExcludeTestTypesPredicate( ReferencePredicateBuilder builder, string suffix ) : base( builder )
    {
        this._suffix = suffix;
    }

    protected override bool IsMatchCore( ReferenceValidationContext context ) => context.Origin.Type.Name.EndsWith( this._suffix );

    protected override ReferenceGranularity GetGranularity() => ReferenceGranularity.Type;
}
