using System.Windows;
using Metalama.Patterns.Wpf;
namespace Doc.Command.CanExecute;
public class MyWindow : Window
{
  public int Counter { get; private set; }
  public bool CanExecuteIncrement => this.Counter < 10;
  public bool CanExecuteDecrement => this.Counter > 0;
  [Command]
  public void Increment()
  {
    this.Counter++;
  }
  [Command]
  public void Decrement()
  {
    this.Counter--;
  }
  public MyWindow()
  {
    IncrementCommand = DelegateCommandFactory.CreateDelegateCommand(Increment, () => CanExecuteIncrement);
    DecrementCommand = DelegateCommandFactory.CreateDelegateCommand(Decrement, () => CanExecuteDecrement);
  }
  public DelegateCommand DecrementCommand { get; }
  public DelegateCommand IncrementCommand { get; }
}