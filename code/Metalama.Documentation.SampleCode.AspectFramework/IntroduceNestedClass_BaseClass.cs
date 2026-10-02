// This is public domain Metalama sample code.

namespace Doc.IntroduceNestedClass_BaseClass;

[Builder]
internal class Material
{
    public string Name { get; }

    public double Density { get; }
}

public abstract class BaseFactory { }