using System.Windows;
using Metalama.Patterns.Wpf;
namespace Doc.Command.SimpleCommand;
public class MyWindow : Window
{
  public int Counter { get; private set; }
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
    IncrementCommand = DelegateCommandFactory.CreateDelegateCommand(Increment, null);
    DecrementCommand = DelegateCommandFactory.CreateDelegateCommand(Decrement, null);
  }
  public DelegateCommand DecrementCommand { get; }
  public DelegateCommand IncrementCommand { get; }
}