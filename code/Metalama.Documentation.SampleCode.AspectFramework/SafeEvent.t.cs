using System;
using Metalama.Framework.RunTime;
namespace Doc.SafeEvent_;
public class Camera
{
  private static readonly ActionEventBrokerDelegateSet<EventHandler?, (object? , EventArgs)> FocusChangedDelegateSet_0 = new ActionEventBrokerDelegateSet<EventHandler?, (object? , EventArgs)>(static (handler, me, args) => ((Camera)me).FocusChanged_Raise_SafeEvent(handler, args), static b => (sender, e) => b.Invoke((sender, e)), static (handler, me) => ((Camera)me).FocusChanged_SafeEvent += handler, static (handler, me) => ((Camera)me).FocusChanged_SafeEvent -= handler);
  private static readonly ActionEventBrokerDelegateSet<EventHandler?, (object? , EventArgs)> LightingChangedDelegateSet_0 = new ActionEventBrokerDelegateSet<EventHandler?, (object? , EventArgs)>(static (handler, me, args) => ((Camera)me).LightingChanged_Raise_SafeEvent(handler, args), static b => (sender, e) => b.Invoke((sender, e)), static (handler, me) => ((Camera)me).LightingChanged_SafeEvent += handler, static (handler, me) => ((Camera)me).LightingChanged_SafeEvent -= handler);
  private EventHandler? _lightingChanged;
  private event EventHandler? _focusChanged;
  private volatile ActionEventBroker<EventHandler?, (object? , EventArgs)>? _focusChangedBroker;
  // Field-like event.
  [SafeEvent]
  public event EventHandler? FocusChanged
  {
    add
    {
      ActionEventBroker<EventHandler?, (object? , EventArgs)>.EnsureInitialized(ref this._focusChangedBroker, this, FocusChangedDelegateSet_0);
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
  private void FocusChanged_Raise_SafeEvent(EventHandler? handler, (object? sender, EventArgs e) args)
  {
    try
    {
      handler.Invoke(args.sender, args.e);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      FocusChanged -= handler;
    }
  }
  private void OnFocusChanged()
  {
    this._focusChanged?.Invoke(this, EventArgs.Empty);
  }
  private volatile ActionEventBroker<EventHandler?, (object? , EventArgs)>? _lightingChangedBroker;
  // Explicitly-implemented event.
  [SafeEvent]
  public event EventHandler? LightingChanged
  {
    add
    {
      ActionEventBroker<EventHandler?, (object? , EventArgs)>.EnsureInitialized(ref this._lightingChangedBroker, this, LightingChangedDelegateSet_0);
      this._lightingChangedBroker.AddHandler(value);
    }
    remove
    {
      this._lightingChangedBroker?.RemoveHandler(value);
    }
  }
  private event EventHandler? LightingChanged_SafeEvent
  {
    add
    {
      this._lightingChanged += value;
    }
    remove
    {
      this._lightingChanged -= value;
    }
  }
  private void LightingChanged_Raise_SafeEvent(EventHandler? handler, (object? sender, EventArgs e) args)
  {
    try
    {
      handler.Invoke(args.sender, args.e);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      LightingChanged -= handler;
    }
  }
  private void OnLightingChanged()
  {
    this._lightingChanged?.Invoke(this, EventArgs.Empty);
  }
}