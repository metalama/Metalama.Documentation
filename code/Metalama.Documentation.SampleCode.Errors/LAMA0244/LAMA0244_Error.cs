// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0244.Error;

// A run-time interface, because it has no scope attribute.
public interface INameFormatter
{
    string Format( string name );
}

// Error: a compile-time class cannot implement the run-time interface 'INameFormatter'.
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
