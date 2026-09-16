using Metalama.Patterns.Contracts;
using System;
using System.Numerics;
namespace Doc.GenericMathContracts;
public static class MathUtilities
{
  public static T Clamp<T>([NonNegative] T value, [StrictlyPositive] T max)
    where T : INumber<T>
  {
    if (value < T.Zero)
    {
      throw new ArgumentOutOfRangeException("value", value, "The 'value' parameter must be greater than or equal to 0.");
    }
    if (max <= T.Zero)
    {
      throw new ArgumentOutOfRangeException("max", max, "The 'max' parameter must be strictly greater than 0.");
    }
    return T.Clamp(value, T.Zero, max);
  }
  public static T Scale<T>(T value, [Range(1, 100)] T percentage)
    where T : INumber<T>
  {
    if (percentage < T.CreateChecked(1) || percentage > T.CreateChecked(100))
    {
      throw new ArgumentOutOfRangeException("percentage", percentage, "The 'percentage' parameter must be in the range [1, 100].");
    }
    return value * percentage / T.CreateChecked(100);
  }
}
