// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0903.Error;

[DerivedTypesMustRespectNamingConvention( "*Repository" )]
public abstract class RepositoryBase { }

// Warning: the name does not match the *Repository pattern.
internal class CustomerStore : RepositoryBase { }
