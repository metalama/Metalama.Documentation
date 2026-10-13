// This is public domain Metalama sample code.

using System;
using System.Windows;
using Metalama.Patterns.Wpf;

namespace Doc.DependencyProperties.NamingConvention;

public class MojeOkno : Window
{
    [DependencyProperty]
    public double ŠířkaRámečku { get; set; }

    // No, víme, skloňovat neumíme.
    private void KontrolovatŠířkaRámečku( double value )
    {
        if ( value < 0 )
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}