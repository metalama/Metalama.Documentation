// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using System;

namespace Doc.LAMA0701.Error;

public interface IClock
{
    DateTime Now { get; }
}

public partial class InvoiceService
{
    // Error: no dependency injection framework is registered to handle this dependency.
    [Dependency]
    private IClock _clock;

    public DateTime GetDueDate() => this._clock.Now.AddDays( 30 );
}
