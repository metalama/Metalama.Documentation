using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5162.Fixed;
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
  public decimal Total => this.ApplyDiscount(this.Subtotal);
  // Fixed: the result depends only on the argument, so the method is marked as constant.
  [Constant]
  private decimal ApplyDiscount(decimal amount) => amount > 100 ? amount * 0.9m : amount;
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}