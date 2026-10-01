using System;
using Microsoft.Win32;
namespace Doc.RegistryStorage;
[RegistryStorage("Animals")]
internal class Animals
{
  private int _turtles;
  public int Turtles
  {
    get
    {
      var value = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Company\\Product\\Animals", "Turtles", null);
      if (value != null)
      {
        return (int)Convert.ChangeType(value, typeof(int));
      }
      else
      {
        return default;
      }
    }
    set
    {
      var stringValue = Convert.ToString(value);
      Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Company\\Product\\Animals", "Turtles", stringValue);
      _turtles = value;
    }
  }
  private int _cats;
  public int Cats
  {
    get
    {
      var value = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Company\\Product\\Animals", "Cats", null);
      if (value != null)
      {
        return (int)Convert.ChangeType(value, typeof(int));
      }
      else
      {
        return default;
      }
    }
    set
    {
      var stringValue = Convert.ToString(value);
      Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Company\\Product\\Animals", "Cats", stringValue);
      _cats = value;
    }
  }
  public int All => this.Turtles + this.Cats;
}