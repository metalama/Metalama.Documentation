// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.Windows.Controls;

namespace Doc.DependencyProperties.ReadOnly;

internal class MyControl : UserControl
{
    [DependencyProperty]
    public double BorderWidth { get; private set; }
}