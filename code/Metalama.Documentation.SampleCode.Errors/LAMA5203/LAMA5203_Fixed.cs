// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.Windows;

namespace Doc.LAMA5203.Fixed;

public partial class ProductControl : DependencyObject
{
    // Fixed: the aspect introduces the QuantityProperty field.
    [DependencyProperty]
    public int Quantity { get; set; }
}
