// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LAMA0203.Error;

public class PropertyTypesAttribute : TypeAspect
{
    // Introduces a method that maps property names to type names, ignoring the case of property names.
    [Introduce]
    public Dictionary<string, string> GetPropertyTypes()
    {
        // Error: the dictionary uses a custom comparer, so it can't be converted to a run-time value.
        var propertyTypes = meta.Target.Type.Properties.ToDictionary(
            p => p.Name,
            p => p.Type.ToDisplayString(),
            new IgnoreCaseComparer() );

        return propertyTypes;
    }
}

[RunTimeOrCompileTime]
public class IgnoreCaseComparer : IEqualityComparer<string>
{
    public bool Equals( string? x, string? y ) => string.Equals( x, y, StringComparison.OrdinalIgnoreCase );

    public int GetHashCode( string obj ) => StringComparer.OrdinalIgnoreCase.GetHashCode( obj );
}
