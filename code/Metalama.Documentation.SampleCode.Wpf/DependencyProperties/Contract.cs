// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;
using Metalama.Patterns.Wpf;
using System.Windows.Controls;

namespace Doc.DependencyProperties.Contract;

internal class MyControl : UserControl
{
    [DependencyProperty]
    [NonNegative]
    public double BorderWidth { get; set; }
}