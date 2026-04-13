namespace Doc.IntroduceExtensionBlock;
[AddStringExtensions]
public static class MyExtensions
{
  extension(string self)
  {
    public bool IsNullOrBlank()
    {
      return false;
    }
  }
}