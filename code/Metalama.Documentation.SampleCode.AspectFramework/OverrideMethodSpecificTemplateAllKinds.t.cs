using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Doc.OverrideMethodSpecificTemplateAllKinds;
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
    foreach (var item in Program.EnumerableMethod_Source())
    {
      Console.WriteLine($"EnumerableMethod: intercepted {item}.");
      yield return item;
    }
    Console.WriteLine("EnumerableMethod: completed.");
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
    using (var enumerator = Program.EnumeratorMethod_Source())
    {
      while (enumerator.MoveNext())
      {
        Console.WriteLine($"EnumeratorMethod: intercepted {enumerator.Current}.");
        yield return enumerator.Current;
      }
    }
    Console.WriteLine("EnumeratorMethod: completed.");
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
    await foreach (var item in Program.AsyncEnumerableMethod_Source())
    {
      Console.WriteLine($"AsyncEnumerableMethod: intercepted {item}.");
      yield return item;
    }
    Console.WriteLine("AsyncEnumerableMethod: completed.");
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
    await using (var enumerator = Program.AsyncEnumeratorMethod_Source())
    {
      while (await enumerator.MoveNextAsync())
      {
        Console.WriteLine($"AsyncEnumeratorMethod: intercepted {enumerator.Current}.");
        yield return enumerator.Current;
      }
    }
    Console.WriteLine("AsyncEnumeratorMethod: completed.");
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