// This is public domain Metalama sample code.

using System.Collections.Generic;

namespace Doc.MixedInitialization;

public enum LifecycleState
{
    BeingConstructed,
    Constructed,
    FullyInitialized
}

public static class LifecycleRegistry
{
    private static readonly Dictionary<object, LifecycleState> _states = new();

    public static void SetState( object instance, LifecycleState state )
    {
        lock ( _states )
        {
            _states[instance] = state;
        }
    }

    public static LifecycleState? GetState( object instance )
    {
        lock ( _states )
        {
            return _states.TryGetValue( instance, out var state ) ? state : null;
        }
    }
}
