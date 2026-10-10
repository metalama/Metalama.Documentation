// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0504.Fixed;

public class DescribableAttribute : TypeAspect
{
    [Introduce]
    public string Describe()
    {
        return $"An instance of {meta.Target.Type.Name}.";
    }
}
