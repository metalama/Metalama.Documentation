// Warning LAMA0903 on `CustomerStore`: `The type 'CustomerStore' does not respect the naming convention set on the base class or interface 'RepositoryBase'. The type name should match the "*Repository" pattern.`
using Metalama.Extensions.Architecture.Aspects;
namespace Doc.LAMA0903.Error;
[DerivedTypesMustRespectNamingConvention("*Repository")]
public abstract class RepositoryBase
{
}
// Warning: the name does not match the *Repository pattern.
internal class CustomerStore : RepositoryBase
{
}