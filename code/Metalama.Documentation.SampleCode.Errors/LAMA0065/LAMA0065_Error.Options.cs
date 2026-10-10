// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using Metalama.Framework.Options;

namespace Doc.LAMA0065.Error;

// Options for the [Log] aspect.
public class LoggingOptions : IHierarchicalOptions<INamedType>, IEligible<INamedType>
{
    public string? Category { get; init; }

    object IIncrementalObject.ApplyChanges( object changes, in ApplyChangesContext context )
    {
        var other = (LoggingOptions) changes;

        return new LoggingOptions { Category = other.Category ?? this.Category };
    }

    public void BuildEligibility( IEligibilityBuilder<INamedType> builder )
    {
        // The options can only be set on non-static types.
        builder.MustNotBeStatic();
    }
}
