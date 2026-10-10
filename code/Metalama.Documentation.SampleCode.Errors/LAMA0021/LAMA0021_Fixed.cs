// This is public domain Metalama sample code.

using Doc.LAMA0021.Fixed;
using Metalama.Framework.Aspects;

// Fixed: a single attribute specifies the order of the two aspects.
[assembly: AspectOrder( AspectOrderDirection.RunTime, typeof(LogAttribute), typeof(MeasureAttribute) )]

namespace Doc.LAMA0021.Fixed;

internal class Calculator
{
    [Log]
    [Measure]
    public int Add( int a, int b ) => a + b;
}
