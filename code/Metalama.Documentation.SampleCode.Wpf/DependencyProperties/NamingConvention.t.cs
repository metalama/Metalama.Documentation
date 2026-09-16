using System;
using System.Windows;
using Metalama.Patterns.Wpf;
namespace Doc.DependencyProperties.NamingConvention;
public class MojeOkno : Window
{
  [DependencyProperty]
  public double ŠířkaRámečku
  {
    get
    {
      return (double)GetValue(ŠířkaRámečkuProperty);
    }
    set
    {
      this.SetValue(ŠířkaRámečkuProperty, value);
    }
  }
  // No, víme, skloňovat neumíme.
  private void KontrolovatŠířkaRámečku(double value)
  {
    if (value < 0)
    {
      throw new ArgumentOutOfRangeException();
    }
  }
  public static readonly DependencyProperty ŠířkaRámečkuProperty;
  static MojeOkno()
  {
    ŠířkaRámečkuProperty = DependencyProperty.Register("ŠířkaRámečku", typeof(double), typeof(MojeOkno), new PropertyMetadata() { CoerceValueCallback = (d, value_1) =>
    {
      ((MojeOkno)d).KontrolovatŠířkaRámečku((double)value_1);
      return value_1;
    } });
  }
}