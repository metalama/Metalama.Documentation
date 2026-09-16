using Metalama.Patterns.Wpf;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.DefaultValue;
internal class MyControl : UserControl
{
  [DependencyProperty]
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
    BorderWidthProperty = DependencyProperty.Register("BorderWidth", typeof(double), typeof(MyControl), new PropertyMetadata((double)5));
  }
  public MyControl()
  {
    BorderWidth = 5;
  }
}