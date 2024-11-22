using System;

namespace Doc.Decoupled_Ref;

[AttributeUsage( AttributeTargets.Method | AttributeTargets.Property )]
public class LogAttribute : Attribute
{
    public string Category { get; set; } = "default";
}
