using System;
using System.IO;
namespace Doc.LAMA0247.Fixed;
internal class FileStore
{
  [RetryOnFailure]
  public bool TryDelete(string path)
  {
    bool Execute()
    {
      if (!File.Exists(path))
      {
        return false;
      }
      File.Delete(path);
      return true;
    }
    var succeeded = Execute();
    if (!succeeded)
    {
      Console.WriteLine("Retrying once.");
      succeeded = Execute();
    }
    return succeeded;
  }
  // Fixed: the aspect is applied only to methods that return bool.
  public void Clear() => Console.WriteLine("Clearing the store.");
}