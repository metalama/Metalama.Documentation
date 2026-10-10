using System;
namespace Doc.LAMA0271.Fixed;
[Extensions]
internal static partial class ObjectExtensions
{
  public static void PrintDetails(this object self)
  {
    Console.WriteLine(self);
  }
}