// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doc.OverrideMethodSpecificTemplateAllKinds;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );
        var result = meta.Proceed();
        Console.WriteLine( $"{meta.Target.Method.Name}: returning {result}." );

        return result;
    }

    public override async Task<dynamic?> OverrideAsyncMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );
        var result = await meta.ProceedAsync();
        Console.WriteLine( $"{meta.Target.Method.Name}: returning {result}." );

        return result;
    }

    public override IEnumerable<dynamic?> OverrideEnumerableMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );

        foreach ( var item in meta.ProceedEnumerable() )
        {
            Console.WriteLine( $"{meta.Target.Method.Name}: intercepted {item}." );

            yield return item;
        }

        Console.WriteLine( $"{meta.Target.Method.Name}: completed." );
    }

    public override IEnumerator<dynamic?> OverrideEnumeratorMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );

        using ( var enumerator = meta.ProceedEnumerator() )
        {
            while ( enumerator.MoveNext() )
            {
                Console.WriteLine(
                    $"{meta.Target.Method.Name}: intercepted {enumerator.Current}." );

                yield return enumerator.Current;
            }
        }

        Console.WriteLine( $"{meta.Target.Method.Name}: completed." );
    }

    public override async IAsyncEnumerable<dynamic?> OverrideAsyncEnumerableMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );

        await foreach ( var item in meta.ProceedAsyncEnumerable() )
        {
            Console.WriteLine( $"{meta.Target.Method.Name}: intercepted {item}." );

            yield return item;
        }

        Console.WriteLine( $"{meta.Target.Method.Name}: completed." );
    }

    public override async IAsyncEnumerator<dynamic?> OverrideAsyncEnumeratorMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );

        await using ( var enumerator = meta.ProceedAsyncEnumerator() )
        {
            while ( await enumerator.MoveNextAsync() )
            {
                Console.WriteLine(
                    $"{meta.Target.Method.Name}: intercepted {enumerator.Current}." );

                yield return enumerator.Current;
            }
        }

        Console.WriteLine( $"{meta.Target.Method.Name}: completed." );
    }
}