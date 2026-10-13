// Warning LAMA1012 on `ApplyDiscount`: `The call to 'PriceCalculator.ApplyDiscount(decimal)' cannot be intercepted by the interceptor method 'DiscountedPriceCalculator.ApplyDiscount(decimal)' registered by the fabric Doc.LAMA1012.Error.Fabric because it is in a lambda that is converted to an expression tree, which describes the call instead of making it. The call site is left unchanged.`
using System;
using System.Linq.Expressions;
namespace Doc.LAMA1012.Error;
internal static class PriceCalculator
{
  public static decimal ApplyDiscount(decimal price) => price * 0.9m;
}
internal static class DiscountedPriceCalculator
{
  public static decimal ApplyDiscount(decimal price) => price * 0.8m;
}
internal static class Checkout
{
  public static void Run()
  {
    // The call is in an expression tree, so Metalama cannot intercept it.
    Expression<Func<decimal>> expression = () => PriceCalculator.ApplyDiscount(100m);
    Console.WriteLine(expression.Compile()());
  }
}