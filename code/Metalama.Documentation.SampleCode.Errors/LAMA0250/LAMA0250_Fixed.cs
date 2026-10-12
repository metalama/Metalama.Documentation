// This is public domain Metalama sample code.

namespace Doc.LAMA0250.Fixed;

public static class FeatureFlags
{
    public static bool IsReportingEnabled { get; set; }
}

internal class ReportService
{
    [SkipIfDisabled]
    public int CountPendingReports() => 42;
}
