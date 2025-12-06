using System;
using System.IO;
using System.Threading;
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
namespace Doc.Disposable;
[Disposable]
internal class Foo : IDisposable
{
  private CancellationTokenSource _cancellationTokenSource = new();
  public void Dispose()
  {
    this.Dispose(true);
  }
  protected virtual void Dispose(bool disposing)
  {
    _cancellationTokenSource?.Dispose();
  }
}
[Disposable]
internal class Bar : Foo
{
  private MemoryStream _stream = new();
  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    _stream?.Dispose();
  }
}