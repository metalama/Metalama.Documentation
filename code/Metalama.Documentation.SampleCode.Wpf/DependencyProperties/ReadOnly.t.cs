using Metalama.Patterns.Wpf;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.ReadOnly;
internal class MyControl : UserControl
{
  [DependencyProperty]
  public double BorderWidth
  {
    get
    {
      return (double)GetValue(BorderWidthProperty);
    }
    private set
    {
      this.SetValue(BorderWidthPropertyKey, value);
    }
  }
  public static readonly DependencyProperty BorderWidthProperty;
  private static readonly DependencyPropertyKey BorderWidthPropertyKey;
  static MyControl()
  {
    BorderWidthPropertyKey = DependencyProperty.RegisterReadOnly("BorderWidth", typeof(double), typeof(MyControl), null);
    BorderWidthProperty = BorderWidthPropertyKey.DependencyProperty;
  }
}