// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0045.Error;

// Error: a live template needs a parameterless constructor, but this aspect has none.
[EditorExperience( SuggestAsLiveTemplate = true )]
public class LogAttribute : OverrideMethodAspect
{
    private readonly string _category;

    public LogAttribute( string category )
    {
        this._category = category;
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this._category}] Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
