// This is public domain Metalama sample code.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doc.OverrideMethodDefaultTemplateAllKinds;

public class Program
{
    [Log]
    public static int NormalMethod()
    {
        return 5;
    }

    [Log]
    public static async Task<int> AsyncMethod()
    {
        Console.WriteLine( "  Task.Yield" );
        await Task.Yield();
        Console.WriteLine( "  Resuming" );

        return 5;
    }

    [Log]
    public static IEnumerable<int> EnumerableMethod()
    {
        Console.WriteLine( "  Yielding 1" );

        yield return 1;

        Console.WriteLine( "  Yielding 2" );

        yield return 2;

        Console.WriteLine( "  Yielding 3" );

        yield return 3;
    }

    [Log]
    public static IEnumerator<int> EnumeratorMethod()
    {
        Console.WriteLine( "  Yielding 1" );

        yield return 1;

        Console.WriteLine( "  Yielding 2" );

        yield return 2;

        Console.WriteLine( "  Yielding 3" );

        yield return 3;
    }

    [Log]
    public static async IAsyncEnumerable<int> AsyncEnumerableMethod()
    {
        await Task.Yield();
        Console.WriteLine( "  Yielding 1" );

        yield return 1;

        Console.WriteLine( "  Yielding 2" );

        yield return 2;

        Console.WriteLine( "  Yielding 3" );

        yield return 3;
    }

    [Log]
    public static async IAsyncEnumerator<int> AsyncEnumeratorMethod()
    {
        await Task.Yield();
        Console.WriteLine( "  Yielding 1" );

        yield return 1;

        Console.WriteLine( "  Yielding 2" );

        yield return 2;

        Console.WriteLine( "  Yielding 3" );

        yield return 3;
    }

    public static async Task Main()
    {
        NormalMethod();

        await AsyncMethod();

        foreach ( var a in EnumerableMethod() )
        {
            Console.WriteLine( $" Received {a} from EnumerableMethod" );
        }

        Console.WriteLine( "---" );

        var enumerator = EnumeratorMethod();

        while ( enumerator.MoveNext() )
        {
            Console.WriteLine( $" Received {enumerator.Current} from EnumeratorMethod" );
        }

        Console.WriteLine( "---" );

        await foreach ( var a in AsyncEnumerableMethod() )
        {
            Console.WriteLine( $" Received {a} from AsyncEnumerableMethod" );
        }

        Console.WriteLine( "---" );
        var asyncEnumerator = AsyncEnumeratorMethod();

        while ( await asyncEnumerator.MoveNextAsync() )
        {
            Console.WriteLine( $" Received {asyncEnumerator.Current} from AsyncEnumeratorMethod" );
        }
    }
}