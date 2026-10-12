// This is public domain Metalama sample code.

using Metalama.Framework.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Doc.AspectConfiguration_Provider;

[AttributeUsage( AttributeTargets.Assembly | AttributeTargets.Class )]
public class LogConfigurationAttribute : Attribute, IHierarchicalOptionsProvider
{
    private readonly TraceLevel? _level;

    public string? Category { get; init; }

    public TraceLevel Level
    {
        get => this._level ?? TraceLevel.Verbose;
        init => this._level = value;
    }

    public IEnumerable<IHierarchicalOptions> GetOptions( in OptionsProviderContext context )
        => new[] { new LoggingOptions() { Category = this.Category, Level = this._level } };
}