// This is public domain Metalama sample code.

using Doc.LAMA0021.Error;
using Metalama.Framework.Aspects;

[assembly: AspectOrder( AspectOrderDirection.RunTime, typeof(LogAttribute), typeof(MeasureAttribute) )]

// Error: this attribute specifies the opposite order, which creates a cycle.
[assembly: AspectOrder( AspectOrderDirection.RunTime, typeof(MeasureAttribute), typeof(LogAttribute) )]

namespace Doc.LAMA0021.Error;

internal class Calculator
{
    [Log]
    [Measure]
    public int Add( int a, int b ) => a + b;
}
