// Warning LAMA5164 on `UnitPrice`: `The 'OrderLine.UnitPrice' field cannot be observed: only private instance fields, constants, and read-only fields of immutable types are supported. Consider replacing the field with a property, or making it private.`
using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5164.Error;
[Observable]
public partial class OrderLine : INotifyPropertyChanged
{
  private decimal _unitPrice;
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
  // Warning: UnitPrice is a public field, so changes to it can't be observed.
  public decimal Total => this.UnitPrice * this.Quantity;
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}