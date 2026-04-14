// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;
using System.Numerics;

namespace Doc.GenericMathContracts;

public static class MathUtilities
{
    public static T Clamp<T>( [NonNegative] T value, [StrictlyPositive] T max )
        where T : INumber<T>
    {
        return T.Clamp( value, T.Zero, max );
    }

    public static T Scale<T>( T value, [Range( 1, 100 )] T percentage )
        where T : INumber<T>
    {
        return value * percentage / T.CreateChecked( 100 );
    }
}
