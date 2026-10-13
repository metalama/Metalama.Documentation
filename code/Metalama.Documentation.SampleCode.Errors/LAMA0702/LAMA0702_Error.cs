// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using System;

namespace Doc.LAMA0702.Error;

public interface IClock
{
    DateTime Now { get; }
}

public partial class InvoiceService
{
    // Error: the default framework can't handle a static dependency.
    [Dependency]
    private static IClock _clock;

    public DateTime GetDueDate() => _clock.Now.AddDays( 30 );
}
