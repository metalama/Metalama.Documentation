using Metalama.Extensions.Architecture.Aspects;
namespace Doc.LAMA0903.Fixed;
[DerivedTypesMustRespectNamingConvention("*Repository")]
public abstract class RepositoryBase
{
}
// Fixed: the class is renamed to match the *Repository pattern.
internal class CustomerRepository : RepositoryBase
{
}