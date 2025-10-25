// This is public domain Metalama sample code.

namespace Doc.IntroduceNestedClass;

[Builder]
internal class Material
{
    public string Name { get; }

    public double Density { get; }
}

internal class Metal : Material
{
    public double MeltingPoint { get; }

    public double ElectricalConductivity { get; }
}