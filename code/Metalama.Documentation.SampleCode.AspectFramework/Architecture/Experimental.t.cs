// Warning LAMA0900 on `Foo`: `The 'ExperimentalApi' type is experimental.`
using Metalama.Extensions.Architecture.Aspects;
namespace Doc.Architecture.Experimental;
[Experimental]
public static class ExperimentalApi
{
  public static void Foo()
  {
  }
  public static void Bar()
  {
    // This call is allowed because we are within the experimental class.
    Foo();
  }
}
internal static class ProductionCode
{
  public static void Dummy()
  {
    // This call is reported.
    ExperimentalApi.Foo();
  }
}