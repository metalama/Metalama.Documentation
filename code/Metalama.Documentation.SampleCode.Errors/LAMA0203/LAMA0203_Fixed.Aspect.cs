// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LAMA0203.Fixed;

public class PropertyTypesAttribute : TypeAspect
{
    // Introduces a method that maps property names to type names, ignoring the case of property names.
    [Introduce]
    public Dictionary<string, string> GetPropertyTypes()
    {
        // Fixed: StringComparer.OrdinalIgnoreCase is a predefined comparer that Metalama can convert.
        var propertyTypes = meta.Target.Type.Properties.ToDictionary(
            p => p.Name,
            p => p.Type.ToDisplayString(),
            StringComparer.OrdinalIgnoreCase );

        return propertyTypes;
    }
}
