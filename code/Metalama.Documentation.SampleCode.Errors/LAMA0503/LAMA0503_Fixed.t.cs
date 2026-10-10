using System;
namespace Doc.LAMA0503.Fixed;
internal class Document
{
  public virtual long GetVersion() => 1;
}
// Fixed: the introduced GetVersion method returns long, like the base method.
[Versioned]
internal partial class Contract : Document
{
  public override long GetVersion()
  {
    Console.WriteLine("Getting the version.");
    return base.GetVersion();
  }
}