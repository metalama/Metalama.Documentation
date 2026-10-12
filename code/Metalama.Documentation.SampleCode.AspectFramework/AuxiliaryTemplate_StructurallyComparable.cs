// This is public domain Metalama sample code.

namespace Doc.StructurallyComparable;

[StructuralEquatable]
internal class WineBottle
{
    public string Cepage { get; init; }

    public int Millesime { get; init; }

    public WineProducer Vigneron { get; init; }
}

[StructuralEquatable]
internal class WineProducer
{
    public string Name { get; init; }

    public string Address { get; init; }
}