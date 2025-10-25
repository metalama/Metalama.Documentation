// This is public domain Metalama sample code.

using System;

namespace Doc.EventLogging;

public class Camera
{
    private EventHandler? _lightingChanged;

    // Field-like event.
    [Log]
    public event EventHandler? FocusChanged;

    private void OnFocusChanged()
    {
        this.FocusChanged?.Invoke( this, EventArgs.Empty );
    }

    // Explicitly-implemented event.
    [Log]
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