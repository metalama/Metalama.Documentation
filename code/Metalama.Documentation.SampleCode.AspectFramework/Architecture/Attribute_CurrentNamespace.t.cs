// Warning LAMA0905 on `InternalMethod`: `The 'Foo.InternalMethod()' method cannot be referenced by the 'ForbiddenClass' type.`
using Doc.Architecture.Type_CurrentNamespace.A;
using Metalama.Extensions.Architecture.Aspects;
namespace Doc.Architecture.Type_CurrentNamespace
{
  namespace A
  {
    [InternalsCanOnlyBeUsedFrom(CurrentNamespace = true)]
    public class Foo
    {
      public void PublicMethod()
      {
      }
      internal void InternalMethod()
      {
      }
    }
    public class AllowedClass
    {
      public void M()
      {
        var foo = new Foo();
        // Allowed because public.
        foo.PublicMethod();
        // Allowed because same namespace.
        foo.InternalMethod();
      }
    }
  }
  namespace B
  {
    public class ForbiddenClass
    {
      public void M()
      {
        var foo = new Foo();
        // Allowed because public.
        foo.PublicMethod();
        // Forbidden because different namespace.
        foo.InternalMethod();
      }
    }
  }
}