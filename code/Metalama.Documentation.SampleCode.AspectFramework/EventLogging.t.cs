using System;
namespace Doc.EventLogging;
public class Camera
{
  private EventHandler? _lightingChanged;
  private event EventHandler? _focusChanged;
  // Field-like event.
  [Log]
  public event EventHandler? FocusChanged
  {
    add
    {
      Console.WriteLine($"Adding handler {value.Method}.");
      this._focusChanged += value;
    }
    remove
    {
      Console.WriteLine($"Removing handler {value.Method}.");
      this._focusChanged -= value;
    }
  }
  private void OnFocusChanged()
  {
    this._focusChanged?.Invoke(this, EventArgs.Empty);
  }
  // Explicitly-implemented event.
  [Log]
  public event EventHandler? LightingChanged
  {
    add
    {
      Console.WriteLine($"Adding handler {value.Method}.");
      this._lightingChanged += value;
    }
    remove
    {
      Console.WriteLine($"Removing handler {value.Method}.");
      this._lightingChanged -= value;
    }
  }
  private void OnLightingChanged()
  {
    this._lightingChanged?.Invoke(this, EventArgs.Empty);
  }
}