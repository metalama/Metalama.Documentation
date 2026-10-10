using Metalama.Patterns.Wpf;
using System.Windows;
namespace Doc.LAMA5203.Fixed;
public partial class ProductControl : DependencyObject
{
  // Fixed: the aspect introduces the QuantityProperty field.
  [DependencyProperty]
  public int Quantity
  {
    get
    {
      return (int)GetValue(QuantityProperty);
    }
    set
    {
      this.SetValue(QuantityProperty, value);
    }
  }
  public static readonly DependencyProperty QuantityProperty;
  static ProductControl()
  {
    QuantityProperty = DependencyProperty.Register("Quantity", typeof(int), typeof(ProductControl));
  }
}