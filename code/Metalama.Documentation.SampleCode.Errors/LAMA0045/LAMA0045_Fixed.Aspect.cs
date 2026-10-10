// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0045.Fixed;

[EditorExperience( SuggestAsLiveTemplate = true )]
public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the category is a property, so the aspect has a parameterless constructor.
    public string Category { get; set; } = "General";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
