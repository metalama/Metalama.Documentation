// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Options;
using System.Diagnostics;

namespace Doc.AspectConfiguration_Provider;

// Options for the [Log] aspects.
public class LoggingOptions : IHierarchicalOptions<IMethod>, IHierarchicalOptions<INamedType>,
                              IHierarchicalOptions<INamespace>, IHierarchicalOptions<ICompilation>
{
    public string? Category { get; init; }

    public TraceLevel? Level { get; init; }

    public object ApplyChanges( object changes, in ApplyChangesContext context )
    {
        var other = (LoggingOptions) changes;

        return new LoggingOptions
        {
            Category = other.Category ?? this.Category, Level = other.Level ?? this.Level
        };
    }
}