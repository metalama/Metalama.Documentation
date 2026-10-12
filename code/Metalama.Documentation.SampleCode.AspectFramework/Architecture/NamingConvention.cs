// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.Architecture.NamingConvention;

[DerivedTypesMustRespectNamingConvention( "*Factory" )]
public interface IFactory { }

// This will report a warning because the naming convention is not respected.
internal class ThingCreator : IFactory { }

// This is properly named.
internal class WidgetFactory : IFactory { }