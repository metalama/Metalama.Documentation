using System;
using Doc.CallsOnly;
internal static class MetalamaInterceptors
{
  internal static string TextUtil_Normalize_Interceptor(string text)
  {
    Console.WriteLine("Calling Normalize.");
    return TextUtil.Normalize(text);
  }
}