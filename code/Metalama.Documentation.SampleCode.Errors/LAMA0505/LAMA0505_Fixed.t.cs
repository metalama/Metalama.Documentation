namespace Doc.LAMA0505.Fixed;
// Fixed: the [Describe] aspect introduces a static method.
[Describe]
internal static partial class MathHelper
{
  public static int Square(int x) => x * x;
  public static string Describe()
  {
    return "MathHelper";
  }
}