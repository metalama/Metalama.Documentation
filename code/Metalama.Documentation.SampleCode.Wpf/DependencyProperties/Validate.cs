// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System;
using System.Windows.Controls;

namespace Doc.DependencyProperties.Validate;

internal class MyControl : UserControl
{
    [DependencyProperty]
    public string? Title { get; set; }

    private void ValidateTitle( string value )
    {
        if ( value.Contains( "foo", StringComparison.OrdinalIgnoreCase ) )
        {
            throw new ArgumentOutOfRangeException( nameof(value) );
        }
    }
}