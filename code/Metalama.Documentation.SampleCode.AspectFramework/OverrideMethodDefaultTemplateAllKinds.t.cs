using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Metalama.Framework.RunTime;
namespace Doc.OverrideMethodDefaultTemplateAllKinds;
public class Program
{
  [Log]
  public static int NormalMethod()
  {
    Console.WriteLine("NormalMethod: start");
    int result;
    result = 5;
    Console.WriteLine($"NormalMethod: returning {result}.");
    return result;
  }
  [Log]
  public static async Task<int> AsyncMethod()
  {
    Console.WriteLine("AsyncMethod: start");
    int result;
    Console.WriteLine("  Task.Yield");
    await Task.Yield();
    Console.WriteLine("  Resuming");
    result = 5;
    Console.WriteLine($"AsyncMethod: returning {result}.");
    return result;
  }
  [Log]
  public static IEnumerable<int> EnumerableMethod()
  {
    Console.WriteLine("EnumerableMethod: start");
    var result = EnumerableMethod_Source().Buffer();
    Console.WriteLine($"EnumerableMethod: returning {result}.");
    return result;
  }
  private static IEnumerable<int> EnumerableMethod_Source()
  {
    Console.WriteLine("  Yielding 1");
    yield return 1;
    Console.WriteLine("  Yielding 2");
    yield return 2;
    Console.WriteLine("  Yielding 3");
    yield return 3;
  }
  [Log]
  public static IEnumerator<int> EnumeratorMethod()
  {
    Console.WriteLine("EnumeratorMethod: start");
    var result = EnumeratorMethod_Source().Buffer();
    Console.WriteLine($"EnumeratorMethod: returning {result}.");
    return result;
  }
  private static IEnumerator<int> EnumeratorMethod_Source()
  {
    Console.WriteLine("  Yielding 1");
    yield return 1;
    Console.WriteLine("  Yielding 2");
    yield return 2;
    Console.WriteLine("  Yielding 3");
    yield return 3;
  }
  [Log]
  public static async IAsyncEnumerable<int> AsyncEnumerableMethod()
  {
    Console.WriteLine("AsyncEnumerableMethod: start");
    var result = await AsyncEnumerableMethod_Source().BufferAsync();
    Console.WriteLine($"AsyncEnumerableMethod: returning {result}.");
    await foreach (var r in result)
    {
      yield return r;
    }
    yield break;
  }
  private static async IAsyncEnumerable<int> AsyncEnumerableMethod_Source()
  {
    await Task.Yield();
    Console.WriteLine("  Yielding 1");
    yield return 1;
    Console.WriteLine("  Yielding 2");
    yield return 2;
    Console.WriteLine("  Yielding 3");
    yield return 3;
  }
  [Log]
  public static async IAsyncEnumerator<int> AsyncEnumeratorMethod()
  {
    Console.WriteLine("AsyncEnumeratorMethod: start");
    var result = await AsyncEnumeratorMethod_Source().BufferAsync();
    Console.WriteLine($"AsyncEnumeratorMethod: returning {result}.");
    await using (result)
    {
      while (await result.MoveNextAsync())
      {
        yield return result.Current;
      }
    }
    yield break;
  }
  private static async IAsyncEnumerator<int> AsyncEnumeratorMethod_Source()
  {
    await Task.Yield();
    Console.WriteLine("  Yielding 1");
    yield return 1;
    Console.WriteLine("  Yielding 2");
    yield return 2;
    Console.WriteLine("  Yielding 3");
    yield return 3;
  }
  public static async Task Main()
  {
    NormalMethod();
    await AsyncMethod();
    foreach (var a in EnumerableMethod())
    {
      Console.WriteLine($" Received {a} from EnumerableMethod");
    }
    Console.WriteLine("---");
    var enumerator = EnumeratorMethod();
    while (enumerator.MoveNext())
    {
      Console.WriteLine($" Received {enumerator.Current} from EnumeratorMethod");
    }
    Console.WriteLine("---");
    await foreach (var a in AsyncEnumerableMethod())
    {
      Console.WriteLine($" Received {a} from AsyncEnumerableMethod");
    }
    Console.WriteLine("---");
    var asyncEnumerator = AsyncEnumeratorMethod();
    while (await asyncEnumerator.MoveNextAsync())
    {
      Console.WriteLine($" Received {asyncEnumerator.Current} from AsyncEnumeratorMethod");
    }
  }
}