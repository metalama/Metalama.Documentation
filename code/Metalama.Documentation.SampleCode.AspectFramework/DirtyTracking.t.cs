using System;
namespace Doc.DirtyTracking;
// Base class without the aspect, but with OnPropertyChanged.
internal class Entity
{
  protected virtual void OnPropertyChanged(string propertyName)
  {
    Console.WriteLine($"Entity.OnPropertyChanged({propertyName})");
  }
}
// Derived class with the aspect. The aspect overrides OnPropertyChanged and calls base.
[DirtyTracking]
internal partial class Customer : Entity
{
  private string? _name;
  public string? Name
  {
    get => _name;
    set
    {
      _name = value;
      OnPropertyChanged(nameof(Name));
    }
  }
  public bool IsDirty { get; private set; }
  protected override void OnPropertyChanged(string propertyName)
  {
    IsDirty = true;
    base.OnPropertyChanged(propertyName);
  }
}
// Standalone class with the aspect. The aspect introduces OnPropertyChanged.
[DirtyTracking]
internal partial class StandaloneOrder
{
  private int _quantity;
  public int Quantity
  {
    get => _quantity;
    set
    {
      _quantity = value;
      OnPropertyChanged(nameof(Quantity));
    }
  }
  public bool IsDirty { get; private set; }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    IsDirty = true;
  }
}
