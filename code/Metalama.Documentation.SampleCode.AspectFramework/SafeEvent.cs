// This is public domain Metalama sample code.

#if TEST_OPTIONS
// https://github.com/metalama/Metalama/issues/1229 EventBroker pattern generates warnings.
// @IgnoredDiagnostic(CS8631)
// @IgnoredDiagnostic(CS0420)
// @IgnoredDiagnostic(CS8602)
#endif

using System;

namespace Doc.SafeEvent_;

public class Camera
{
    private EventHandler? _lightingChanged;

    // Field-like event.
    [SafeEvent]
    public event EventHandler? FocusChanged;

    private void OnFocusChanged()
    {
        this.FocusChanged?.Invoke( this, EventArgs.Empty );
    }

    // Explicitly-implemented event.
    [SafeEvent]
    public event EventHandler? LightingChanged
    {
        add => this._lightingChanged += value;
        remove => this._lightingChanged -= value;
    }

    private void OnLightingChanged()
    {
        this._lightingChanged?.Invoke( this, EventArgs.Empty );
    }
}
