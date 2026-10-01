// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Options;
using System;
using System.Diagnostics;

namespace Doc.AspectConfiguration_ProjectDefault;

// Options for the [Log] aspects.
public class LoggingOptions : IHierarchicalOptions<IMethod>, IHierarchicalOptions<INamedType>,
                              IHierarchicalOptions<INamespace>, IHierarchicalOptions<ICompilation>
{
    private static readonly DiagnosticDefinition<string> _invalidLogLevelWarning = new(
        "MY001",
        Severity.Warning,
        "The 'DefaultLogLevel' MSBuild property was set to the invalid value '{0}' and has been ignored." );

    public string? Category { get; init; }

    public TraceLevel? Level { get; init; }

    object IIncrementalObject.ApplyChanges( object changes, in ApplyChangesContext context )
    {
        var other = (LoggingOptions) changes;

        return new LoggingOptions
        {
            Category = other.Category ?? this.Category, Level = other.Level ?? this.Level
        };
    }

    IHierarchicalOptions IHierarchicalOptions.GetDefaultOptions(
        OptionsInitializationContext context )
    {
        context.Project.TryGetProperty( "DefaultLogCategory", out var defaultCategory );

        if ( string.IsNullOrWhiteSpace( defaultCategory ) )
        {
            defaultCategory = "Trace";
        }
        else
        {
            defaultCategory = defaultCategory.Trim();
        }

        TraceLevel defaultLogLevel;

        context.Project.TryGetProperty( "DefaultLogLevel", out var defaultLogLevelString );

        if ( string.IsNullOrWhiteSpace( defaultLogLevelString ) )
        {
            defaultLogLevel = TraceLevel.Verbose;
        }
        else
        {
            if ( !Enum.TryParse( defaultLogLevelString.Trim(), out defaultLogLevel ) )
            {
                context.Diagnostics.Report(
                    _invalidLogLevelWarning.WithArguments( defaultLogLevelString ) );

                defaultLogLevel = TraceLevel.Verbose;
            }
        }

        return new LoggingOptions { Category = defaultCategory, Level = defaultLogLevel };
    }
}