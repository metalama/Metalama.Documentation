// Warning LAMA0905 on `VerifiedNamespace`: `The 'Doc.GettingStarted_Architecture_Ns.VerifiedNamespace' namespace cannot be referenced by the 'Doc.GettingStarted_Architecture_Ns.OtherNamespace' namespace.`
// Warning LAMA0905 on `Foo`: `The 'Doc.GettingStarted_Architecture_Ns.VerifiedNamespace' namespace cannot be referenced by the 'Doc.GettingStarted_Architecture_Ns.OtherNamespace' namespace.`
namespace Doc.GettingStarted_Architecture_Ns
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