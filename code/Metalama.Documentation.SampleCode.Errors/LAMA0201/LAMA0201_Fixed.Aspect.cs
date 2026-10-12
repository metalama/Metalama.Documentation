// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA0201.Fixed;

public class TagsAttribute : TypeAspect
{
    // Introduces a method that returns tags computed at compile time.
    [Introduce]
    public object GetTags()
    {
        var tags = meta.CompileTime( new List<object>() );
        tags.Add( meta.Target.Type.Name );

        // Fixed: the list no longer contains itself.
        tags.Add( meta.Target.Type.FullName );

        // The value is typed as object, so Metalama converts it when it expands the template.
        object result = tags;

        return meta.RunTime( result );
    }
}
