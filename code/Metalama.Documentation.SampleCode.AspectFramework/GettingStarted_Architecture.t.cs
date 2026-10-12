// Warning LAMA0905 on `VerifiedNamespace.Foo`: `The 'Doc.GettingStarted_Architecture.VerifiedNamespace' namespace cannot be referenced by the 'ForbiddenInheritor' type.`
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
    internal class ForbiddenInheritor : VerifiedNamespace.Foo
    {
    }
  }
}