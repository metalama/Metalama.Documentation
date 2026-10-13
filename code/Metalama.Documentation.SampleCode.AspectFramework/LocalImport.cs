// This is public domain Metalama sample code.

using System;

namespace Doc.ImportService;

internal class Foo
{
    // readonly IServiceProvider _serviceProvider;

    [ImportAspect]
    private IFormatProvider? FormatProvider { get; }

    public string Format( object? o )
    {
        return ((ICustomFormatter) this.FormatProvider!.GetFormat( typeof(ICustomFormatter) )!)
            .Format( null, o, this.FormatProvider );
    }
}