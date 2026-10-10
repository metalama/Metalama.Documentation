// Warning LAMA5162 on `ApplyDiscount`: `The 'Order.ApplyDiscount(decimal)' method cannot be observed, and has not been configured with an observability contract. Mark this method with [ConstantAttribute] or call ConfigureObservability via a fabric.`
using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5162.Error;
[Observable]
public partial class Order : INotifyPropertyChanged
{
  private decimal _subtotal;
  public decimal Subtotal
  {
    get
    {
      return _subtotal;
    }
    set
    {
      if (_subtotal != value)
      {
        _subtotal = value;
        OnPropertyChanged("Total");
        OnPropertyChanged("Subtotal");
      }
    }
  }
  // Warning: ApplyDiscount is an instance method of a mutable type, so it isn't known to be constant.
  public decimal Total => this.ApplyDiscount(this.Subtotal);
  private decimal ApplyDiscount(decimal amount) => amount > 100 ? amount * 0.9m : amount;
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}