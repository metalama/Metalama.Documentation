using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5164.Fixed;
[Observable]
public partial class OrderLine : INotifyPropertyChanged
{
  private decimal _unitPrice;
  // Fixed: the public field is replaced with an auto-property.
  public decimal UnitPrice
  {
    get
    {
      return _unitPrice;
    }
    set
    {
      if (_unitPrice != value)
      {
        _unitPrice = value;
        OnPropertyChanged("Total");
        OnPropertyChanged("UnitPrice");
      }
    }
  }
  private int _quantity;
  public int Quantity
  {
    get
    {
      return _quantity;
    }
    set
    {
      if (_quantity != value)
      {
        _quantity = value;
        OnPropertyChanged("Total");
        OnPropertyChanged("Quantity");
      }
    }
  }
  public decimal Total => this.UnitPrice * this.Quantity;
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}