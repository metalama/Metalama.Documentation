// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;
using Metalama.Framework.Utilities;

namespace Doc.Decoupled;

[AttributeUsage( AttributeTargets.Method | AttributeTargets.Property )]
[RunTimeOrCompileTime]
[ImmutableType]
public class LogAttribute : Attribute, ICompileTimeSerializable
{
    public string Category { get; init; } = "default";
}