// This is public domain Metalama sample code.

namespace Doc.LAMA0504.Error;

[Describable]
internal partial class Product
{
    // Error: this static method has the same signature as the instance method introduced by the aspect.
    public static string Describe() => "A product of the catalog.";
}
