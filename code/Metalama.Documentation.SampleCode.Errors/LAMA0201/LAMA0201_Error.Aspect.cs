// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA0201.Error;

public class TagsAttribute : TypeAspect
{
    // Introduces a method that returns tags computed at compile time.
    [Introduce]
    public object GetTags()
    {
        var tags = meta.CompileTime( new List<object>() );
        tags.Add( meta.Target.Type.Name );

        // Error: the list contains itself, so it can't be converted to a run-time value.
        tags.Add( tags );

        // The value is typed as object, so Metalama converts it when it expands the template.
        object result = tags;

        return meta.RunTime( result );
    }
}
