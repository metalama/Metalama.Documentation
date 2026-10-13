// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0750.Fixed;

public class TypeNameAttribute : TypeAspect
{
    [Introduce]
    public string GetTypeName() => meta.Target.Type.Name;
}
