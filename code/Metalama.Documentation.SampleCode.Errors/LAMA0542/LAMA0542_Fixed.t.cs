using System;
namespace Doc.LAMA0542.Fixed;
// The indexer is introduced with a regular set accessor.
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