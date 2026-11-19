// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Utilities;
using System.Collections.Immutable;

namespace Doc.CompileTimeRules_OwnType;

// The policy is stored in an aspect, so it must be immutable and durable too.
// The attributes state these contracts, and the analyzer verifies every member.
[CompileTime]
[ImmutableType]
[Durable]
internal sealed class LogPolicy
{
    public LogPolicy( string category, ImmutableArray<string> hiddenParameters )
    {
        this.Category = category;
        this.HiddenParameters = hiddenParameters;
    }

    public string Category { get; }

    public ImmutableArray<string> HiddenParameters { get; }
}
