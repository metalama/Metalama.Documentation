// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using System;

namespace Doc.LAMA0702.Fixed;

public interface IClock
{
    DateTime Now { get; }
}

public partial class InvoiceService
{
    // Fixed: the dependency is an instance field.
    [Dependency]
    private IClock _clock;

    public DateTime GetDueDate() => this._clock.Now.AddDays( 30 );
}
