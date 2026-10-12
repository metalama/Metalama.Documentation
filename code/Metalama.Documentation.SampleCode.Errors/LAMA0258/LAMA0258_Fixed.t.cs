using System;
namespace Doc.LAMA0258.Fixed;
[TrackFinalization]
internal class FileHandle
{
  ~FileHandle()
  {
    Console.WriteLine("Finalizing the object.");
  }
}