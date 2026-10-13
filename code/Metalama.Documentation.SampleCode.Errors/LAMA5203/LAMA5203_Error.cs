// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.Windows;

namespace Doc.LAMA5203.Error;

public partial class ProductControl : DependencyObject
{
    [DependencyProperty]
    public int Quantity { get; set; }

    // Error: this field has the name of the registration field that the aspect must introduce.
    public static readonly DependencyProperty QuantityProperty =
        DependencyProperty.Register( "Quantity", typeof(int), typeof(ProductControl) );
}
