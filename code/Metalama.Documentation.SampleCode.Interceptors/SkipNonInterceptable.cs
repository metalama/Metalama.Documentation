// This is public domain Metalama sample code.

using System.Collections.Generic;
using System.Linq;

namespace Doc.SkipNonInterceptable;

[TraceCalls]
public class Inventory
{
    public void Reset() => Store.Slot() = 0;

    public IQueryable<int> FindLarge( IQueryable<int> quantities )
        => quantities.Where( q => Store.IsLarge( q ) );

    public IEnumerable<int> FindLarge( IEnumerable<int> quantities )
        => quantities.Where( q => Store.IsLarge( q ) );
}

public static class Store
{
    private static int _slot;

    public static ref int Slot() => ref _slot;

    public static bool IsLarge( int quantity ) => quantity > 100;
}
