using Metalama.Patterns.Memoization;
using System;
using System.IO.Hashing;
using System.Runtime.CompilerServices;
namespace Doc.Memoize_;
public class HashedBuffer
{
  public HashedBuffer(ReadOnlyMemory<byte> buffer)
  {
    this.Buffer = buffer;
  }
  public ReadOnlyMemory<byte> Buffer { get; }
  [Memoize]
  public ReadOnlyMemory<byte> Hash
  {
    get
    {
      if (_Hash == null)
      {
        var value = new StrongBox<ReadOnlyMemory<byte>>(Hash_Source);
        global::System.Threading.Interlocked.CompareExchange(ref this._Hash, value, null);
      }
      return _Hash.Value;
    }
  }
  private ReadOnlyMemory<byte> Hash_Source => XxHash64.Hash(this.Buffer.Span);
  [Memoize]
  public override string ToString()
  {
    if (_ToString == null)
    {
      string value;
      value = $"{{HashedBuffer ({this.Buffer.Length} bytes)}}";
      global::System.Threading.Interlocked.CompareExchange(ref this._ToString, value, null);
    }
    return _ToString;
  }
  private StrongBox<ReadOnlyMemory<byte>> _Hash;
  private string _ToString;
}