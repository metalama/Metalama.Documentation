using Metalama.Patterns.Contracts;
using Metalama.Patterns.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.Contract;
internal class MyControl : UserControl
{
  [DependencyProperty]
  [NonNegative]
  public double BorderWidth
  {
    get
    {
      return (double)GetValue(BorderWidthProperty);
    }
    set
    {
      this.SetValue(BorderWidthProperty, value);
    }
  }
  public static readonly DependencyProperty BorderWidthProperty;
  static MyControl()
  {
    BorderWidthProperty = DependencyProperty.Register("BorderWidth", typeof(double), typeof(MyControl), new PropertyMetadata() { CoerceValueCallback = (d, value) =>
    {
      value = ApplyBorderWidthContracts((double)value);
      return value;
    } });
  }
  private static double ApplyBorderWidthContracts(double value)
  {
    if (value < 0)
    {
      throw new ArgumentOutOfRangeException("value", value, "The 'value' parameter must be greater than or equal to 0.");
    }
    return value;
  }
}