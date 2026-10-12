namespace Doc.LAMA0091.Fixed;
// Fixed: both interceptor aspects are ordered before [Audit].
[InterceptWrite]
[Audit]
[InterceptPrint]
internal static class Program
{
  public static void Run()
  {
    Redirections.Print("a");
    Redirections.Write("b");
  }
}