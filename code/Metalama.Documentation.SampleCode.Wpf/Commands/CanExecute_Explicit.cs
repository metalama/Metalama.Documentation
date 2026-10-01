// This is public domain Metalama sample code.

using System.Windows;
using Metalama.Patterns.Wpf;

namespace Doc.Command.CanExecute_Explicit;

public class MyWindow : Window
{
    public int Counter { get; private set; }

    public bool CanExecuteIncrement => this.Counter < 10;

    public bool CanExecuteDecrement => this.Counter > 0;

    [Command( CanExecuteProperty = nameof(CanExecuteIncrement) )]
    public void Increment()
    {
        this.Counter++;
    }

    [Command( CanExecuteProperty = nameof(CanExecuteDecrement) )]
    public void Decrement()
    {
        this.Counter--;
    }
}