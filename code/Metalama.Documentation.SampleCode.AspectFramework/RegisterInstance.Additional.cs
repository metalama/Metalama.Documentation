// This is public domain Metalama sample code.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Doc.RegisterInstance;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine( "Allocate object." );
        AllocateObject();

        Console.WriteLine( "GC.Collect()" );
        GC.Collect();

        PrintInstances();
    }

    private static void AllocateObject()
    {
        var o = new DemoClass();

        PrintInstances();

        _ = o;
    }

    private static void PrintInstances()
    {
        foreach ( var instance in InstanceRegistry.GetInstances() )
        {
            Console.WriteLine( instance );
        }
    }
}

public static class InstanceRegistry
{
    private static int _nextId;
    private static readonly ConcurrentDictionary<int, WeakReference<object>> _instances = new();

    public static IDisposable Register( object instance )
    {
        var id = Interlocked.Increment( ref _nextId );
        _instances.TryAdd( id, new WeakReference<object>( instance ) );

        return new Handle( id );
    }

    private static void Unregister( int id )
    {
        _instances.TryRemove( id, out _ );
    }

    public static IEnumerable<object> GetInstances()
    {
        foreach ( var weakReference in _instances.Values )
        {
            if ( weakReference.TryGetTarget( out var instance ) )
            {
                yield return instance;
            }
        }
    }

    private class Handle : IDisposable
    {
        private readonly int _id;

        public Handle( int id )
        {
            this._id = id;
        }

        public void Dispose()
        {
            GC.SuppressFinalize( this );
            Unregister( this._id );
        }

        ~Handle()
        {
            Unregister( this._id );
        }
    }
}