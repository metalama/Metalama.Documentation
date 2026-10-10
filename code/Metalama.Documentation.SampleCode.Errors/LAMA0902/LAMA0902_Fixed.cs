// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0902.Fixed;

// Fixed: the pattern is a valid regular expression.
[DerivedTypesMustRespectRegexNamingConvention( "^.*Service$" )]
public abstract class ServiceBase { }

internal class OrderService : ServiceBase { }
