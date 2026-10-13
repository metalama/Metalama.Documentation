// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;

namespace Doc.PassCallerInstance;

[ExcludeInterceptors]
public static class TelemetryWithSender
{
    public static void Track( string eventName, object? sender )
        => Telemetry.Track( $"{eventName} (sender: {sender?.GetType().Name ?? "static code"})" );
}
