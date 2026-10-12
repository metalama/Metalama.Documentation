// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Doc.CountCalls;

[ExcludeInterceptors]
public static class CallCounter
{
    private static readonly ConcurrentDictionary<string, int> _counts = new();

    // [<snippet Signature>]
    public static decimal ComputeTax(
        LegacyTaxCalculator calculator,
        decimal amount,
        string region,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0 )
    // [<endsnippet Signature>]
    {
        _counts.AddOrUpdate( $"{caller}, line {line}", 1, ( _, count ) => count + 1 );

        return calculator.ComputeTax( amount, region );
    }

    public static void Print()
    {
        foreach ( var (location, count) in _counts.OrderBy( p => p.Key ) )
        {
            Console.WriteLine( $"{location}: {count} call(s)" );
        }
    }
}
