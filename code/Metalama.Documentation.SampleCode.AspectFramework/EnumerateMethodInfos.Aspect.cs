// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Collections.Generic;
using System.Reflection;

namespace Doc.EnumerateMethodInfos;

internal class EnumerateMethodAspect : TypeAspect
{
    [Introduce]
    public IReadOnlyList<MethodInfo> GetMethods()
    {
        var methods = new List<MethodInfo>();

        foreach ( var method in meta.Target.Type.Methods )
        {
            methods.Add( method.ToMethodInfo() );
        }

        return methods;
    }
}