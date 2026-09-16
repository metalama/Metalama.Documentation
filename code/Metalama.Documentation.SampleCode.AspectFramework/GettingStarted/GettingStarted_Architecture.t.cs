// Warning LAMA0905 on `Foo`: `The 'Doc.GettingStarted_Architecture.VerifiedNamespace' namespace cannot be referenced by the 'Doc.GettingStarted_Architecture.OtherNamespace' namespace.`
using Doc.GettingStarted_Architecture.VerifiedNamespace;
namespace Doc.GettingStarted_Architecture
{
  namespace VerifiedNamespace
  {
    internal class Foo
    {
    }
    internal class AllowedInheritor : Foo
    {
    }
  }
  namespace OtherNamespace
  {
    internal class ForbiddenInheritor : Foo
    {
    }
  }
}