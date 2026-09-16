// This is public domain Metalama sample code.

using System.IO;
using System.Threading;

#pragma warning disable CA1001 // Types that own disposable fields should be disposable

namespace Doc.Disposable;

[Disposable]
internal class Foo
{
    private CancellationTokenSource _cancellationTokenSource = new();
}

[Disposable]
internal class Bar : Foo
{
    private MemoryStream _stream = new();
}