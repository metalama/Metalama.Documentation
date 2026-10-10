// This is public domain Metalama sample code.

using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0907.Error;

// This class does not derive from ReferencePredicate.
[CompileTime]
internal class ExcludeTestTypesPredicate
{
    public bool IsMatch( ReferenceValidationContext context ) => context.Origin.Type.Name.EndsWith( "Tests" );
}
