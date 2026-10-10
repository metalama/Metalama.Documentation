// Warning LAMA5164 on `UnitPrice`: `The 'OrderLine.UnitPrice' field cannot be observed: only private instance fields of the current type, fields belonging to primitive types, readonly fields of primitive types, and fields configured with an observability contract are supported. Consider accessing the field through a property, marking 'OrderLine.UnitPrice' with [Constant], or using ConfigureObservability via a fabric.`
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