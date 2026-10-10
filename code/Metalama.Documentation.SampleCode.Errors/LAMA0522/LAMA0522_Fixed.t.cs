using System;
namespace Doc.LAMA0522.Fixed;
[TrackFinalization]
internal class FileHandle
{
  public string Path { get; set; } = "";
  ~FileHandle()
  {
    Console.WriteLine("Finalizing an instance of FileHandle.");
  }
}