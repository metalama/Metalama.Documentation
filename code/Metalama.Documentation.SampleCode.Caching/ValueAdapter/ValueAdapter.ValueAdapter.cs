// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.ValueAdapters;
using System.Text;

namespace Doc.ValueAdapter;

internal class StringBuilderAdapter : ValueAdapter<StringBuilder>
{
    public override StringBuilder? GetExposedValue( object? storedValue )
        => storedValue == null ? null : new StringBuilder( (string) storedValue );

    public override object? GetStoredValue( StringBuilder? value ) => value?.ToString();
}