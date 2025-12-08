using System;
using Metalama.Framework.RunTime.Events;
namespace Doc.SafeEvent_;
public class Camera
{
  private static readonly DelegateEventAdapter<EventHandler, (object? , EventArgs), Camera> FocusChangedAdapter_0 = new(static (handler, ref args, me) => me.FocusChanged_Invoke_SafeEvent(handler, ref args), static b => (sender, e) => b.Invoke((sender, e)), static (handler, me) => me.FocusChanged_SafeEvent += handler, static (handler, me) => me.FocusChanged_SafeEvent -= handler);
  private static readonly DelegateEventAdapter<EventHandler, (object? , EventArgs), Camera> LightingChangedAdapter_0 = new(static (handler, ref args, me) => me.LightingChanged_Invoke_SafeEvent(handler, ref args), static b => (sender, e) => b.Invoke((sender, e)), static (handler, me) => me.LightingChanged_SafeEvent += handler, static (handler, me) => me.LightingChanged_SafeEvent -= handler);
  private EventHandler? _lightingChanged;
  private event EventHandler? _focusChanged;
  private volatile EventBroker<EventHandler, (object? , EventArgs), Camera>? _focusChangedBroker;
  // Field-like event.
  [SafeEvent]
  public event EventHandler? FocusChanged
  {
    add
    {
      EventBroker.EnsureInitialized(ref this._focusChangedBroker, FocusChangedAdapter_0, this);
      this._focusChangedBroker.AddHandler(value);
    }
    remove
    {
      this._focusChangedBroker?.RemoveHandler(value);
    }
  }
  private event EventHandler? FocusChanged_SafeEvent
  {
    add
    {
      this._focusChanged += value;
    }
    remove
    {
      this._focusChanged -= value;
    }
  }
  private void FocusChanged_Invoke_SafeEvent(EventHandler? handler, ref (object? sender, EventArgs e) args)
  {
    try
    {
      handler.Invoke(args.sender, args.e);
      return;
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      _focusChanged -= handler;
      return;
    }
  }
  private void OnFocusChanged()
  {
    this._focusChanged?.Invoke(this, EventArgs.Empty);
  }
  private volatile EventBroker<EventHandler, (object? , EventArgs), Camera>? _lightingChangedBroker;
  // Explicitly-implemented event.
  [SafeEvent]
  public event EventHandler? LightingChanged
  {
    add
    {
      EventBroker.EnsureInitialized(ref this._lightingChangedBroker, LightingChangedAdapter_0, this);
      this._lightingChangedBroker.AddHandler(value);
    }
    remove
    {
      this._lightingChangedBroker?.RemoveHandler(value);
    }
  }
  private event EventHandler? LightingChanged_Source { add => this._lightingChanged += value; remove => this._lightingChanged -= value; }
  private event EventHandler? LightingChanged_SafeEvent
  {
    add
    {
      this.LightingChanged_Source += value;
    }
    remove
    {
      this.LightingChanged_Source -= value;
    }
  }
  private void LightingChanged_Invoke_SafeEvent(EventHandler? handler, ref (object? sender, EventArgs e) args)
  {
    try
    {
      handler.Invoke(args.sender, args.e);
      return;
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      LightingChanged_Source -= handler;
      return;
    }
  }
  private void OnLightingChanged()
  {
    this._lightingChanged?.Invoke(this, EventArgs.Empty);
  }
}