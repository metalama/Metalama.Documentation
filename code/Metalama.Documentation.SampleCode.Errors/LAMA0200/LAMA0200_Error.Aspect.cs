// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LAMA0200.Error;

public class MethodOverloadCountAttribute : TypeAspect
{
    // Introduces a method that returns the number of overloads of each method.
    [Introduce]
    public Dictionary<string, MethodOverloadCount> GetMethodOverloadCount()
    {
        var dictionary = meta.Target.Type.Methods
            .GroupBy( m => m.Name )
            .Select( g => new MethodOverloadCount( g.Key, g.Count() ) )
            .ToDictionary( m => m.Name, m => m );

        // Error: MethodOverloadCount can't be converted to a run-time value.
        return dictionary;
    }
}

// This class exists both at compile time and at run time.
[RunTimeOrCompileTime]
public class MethodOverloadCount
{
    public MethodOverloadCount( string name, int count )
    {
        this.Name = name;
        this.Count = count;
    }

    public string Name { get; }

    public int Count { get; }
}
