// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;
using System;

namespace Doc.Skipping;

[Observable]
public class DateTimeViewModel
{
    public DateTime DateTime { get; set; }

    [NotObservable]
    public double MinutesFromNow => (DateTime.Now - this.DateTime).TotalMinutes;
}