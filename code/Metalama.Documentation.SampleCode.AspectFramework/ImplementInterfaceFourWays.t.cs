using System;
namespace Doc.ImplementInterfaceFourWays;
// The aspect introduces IDisposable and a Reset method.
// Reset demonstrates four ways to call the introduced Dispose method.
[GenerateReset]
public partial class MyResource : IDisposable
{
  public void Dispose()
  {
    Console.WriteLine("Disposing...");
  }
  public void Reset()
  {
    // Option 1: this.Dispose()
    Dispose();
    // Option 2: meta.This.Dispose()
    this.Dispose();
    // Option 3: disposeMethod.Invoke()
    ((IDisposable)this).Dispose();
    // Option 4: ((IDisposable)meta.This).Dispose()
    ((IDisposable)this).Dispose();
  }
}
