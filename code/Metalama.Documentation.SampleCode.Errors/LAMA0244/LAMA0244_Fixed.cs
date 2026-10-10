// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0244.Fixed;

// Fixed: [RunTimeOrCompileTime] makes the interface available at compile time.
[RunTimeOrCompileTime]
public interface INameFormatter
{
    string Format( string name );
}

// The compile-time class can now implement the interface.
[CompileTime]
public class UpperCaseNameFormatter : INameFormatter
{
    public string Format( string name ) => name.ToUpperInvariant();
}

internal class Calculator
{
    [Log]
    public int Add( int a, int b ) => a + b;
}
