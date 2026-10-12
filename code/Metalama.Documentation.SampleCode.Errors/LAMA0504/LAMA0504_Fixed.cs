// This is public domain Metalama sample code.

namespace Doc.LAMA0504.Fixed;

[Describable]
internal partial class Product
{
    // Fixed: the static method has a different name, so it no longer conflicts with the introduced method.
    public static string DescribeType() => "A product of the catalog.";
}
