// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Serialization;

namespace Doc.Decoupled;

[AttributeUsage( AttributeTargets.Method | AttributeTargets.Property )]
[RunTimeOrCompileTime]
public class LogAttribute : Attribute, ICompileTimeSerializable
{
    public string Category { get; set; } = "default";
}