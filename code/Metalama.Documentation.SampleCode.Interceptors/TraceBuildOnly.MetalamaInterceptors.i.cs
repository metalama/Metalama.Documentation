using System;
using System.IO;
internal static class MetalamaInterceptors
{
  internal static string File_ReadAllText_Interceptor(string path)
  {
    Console.WriteLine("Calling ReadAllText.");
    return File.ReadAllText(path);
  }
}