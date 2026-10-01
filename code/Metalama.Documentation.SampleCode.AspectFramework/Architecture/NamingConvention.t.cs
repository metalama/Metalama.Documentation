// Warning LAMA0903 on `ThingCreator`: `The type 'ThingCreator' does not respect the naming convention set on the base class or interface 'IFactory'. The type name should match the "*Factory" pattern.`
using Metalama.Extensions.Architecture.Aspects;
namespace Doc.Architecture.NamingConvention;
[DerivedTypesMustRespectNamingConvention("*Factory")]
public interface IFactory
{
}
// This will report a warning because the naming convention is not respected.
internal class ThingCreator : IFactory
{
}
// This is properly named.
internal class WidgetFactory : IFactory
{
}