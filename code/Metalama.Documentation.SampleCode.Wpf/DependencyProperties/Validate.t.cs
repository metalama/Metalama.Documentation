using Metalama.Patterns.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Doc.DependencyProperties.Validate;
internal class MyControl : UserControl
{
  [DependencyProperty]
  public string? Title
  {
    get
    {
      return (string? )GetValue(TitleProperty);
    }
    set
    {
      this.SetValue(TitleProperty, value);
    }
  }
  private void ValidateTitle(string value)
  {
    if (value.Contains("foo"))
    {
      throw new ArgumentOutOfRangeException(nameof(value));
    }
  }
  public static readonly DependencyProperty TitleProperty;
  static MyControl()
  {
    TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MyControl), new PropertyMetadata() { CoerceValueCallback = (d, value_1) =>
    {
      ((MyControl)d).ValidateTitle((string? )value_1);
      return value_1;
    } });
  }
}