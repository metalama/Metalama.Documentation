// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Options;
using System.Collections.Generic;
using System.Diagnostics;

namespace Doc.AspectConfiguration_ProjectDefault;

// The aspect itself, consuming the configuration.
public class LogAttribute : OverrideMethodAspect, IHierarchicalOptionsProvider
{
    private readonly TraceLevel? _level;

    public string? Category { get; init; }

    public TraceLevel Level
    {
        get => this._level ?? TraceLevel.Verbose;
        init => this._level = value;
    }

    public override dynamic? OverrideMethod()
    {
        var options = meta.Target.Method.Enhancements().GetOptions<LoggingOptions>();

        var message = $"{options.Category}: Executing {meta.Target.Method}.";

        switch ( options.Level!.Value )
        {
            case TraceLevel.Error:
                Trace.TraceError( message );

                break;

            case TraceLevel.Info:
                Trace.TraceInformation( message );

                break;

            case TraceLevel.Warning:
                Trace.TraceWarning( message );

                break;

            case TraceLevel.Verbose:
                Trace.WriteLine( message );

                break;
        }

        return meta.Proceed();
    }

    public IEnumerable<IHierarchicalOptions> GetOptions( in OptionsProviderContext context )
        => new[] { new LoggingOptions { Category = this.Category, Level = this._level } };
}