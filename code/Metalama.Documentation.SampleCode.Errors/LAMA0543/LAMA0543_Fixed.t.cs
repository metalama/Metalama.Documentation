using System;
namespace Doc.LAMA0543.Fixed;
[ItemIndexer]
internal static class IntExtensions
{
  extension(int number)
  {
    public string this[int index]
    {
      get
      {
        Console.WriteLine("Get.");
        return index.ToString();
      }
      set
      {
        Console.WriteLine("Set.");
      }
    }
  }
}