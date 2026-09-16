// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.Windows.Controls;

namespace Doc.DependencyProperties.OnPropertyChanged;

internal class MyControl : UserControl
{
    [DependencyProperty]
    public double BorderWidth { get; set; }

    [DependencyProperty]
    public double AvailableWidth { get; private set; }

    private void OnBorderWidthChanged()
    {
        this.AvailableWidth = this.Width - this.BorderWidth * 2;
    }
}