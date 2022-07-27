using Metalama.Framework.Aspects;
using System;

namespace Doc.ConsumingProperty
{

#pragma warning disable CS0067, CS8618, CA1822, CS0162, CS0169, CS0414
    public class Log : OverrideMethodAspect
    {
        public string? Category { get; set; }

        public override dynamic? OverrideMethod() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");

    }

#pragma warning restore CS0067, CS8618, CA1822, CS0162, CS0169, CS0414
}