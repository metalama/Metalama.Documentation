using System.ComponentModel;
using System.Windows;
using Metalama.Patterns.Observability;
using Metalama.Patterns.Wpf;
namespace Doc.Command.CanExecute_Observable;
[Observable]
public class MyWindow : Window, INotifyPropertyChanged
{
  private int _counter;
  public int Counter
  {
    get
    {
      return _counter;
    }
    private set
    {
      if (_counter != value)
      {
        _counter = value;
        OnPropertyChanged("CanExecuteDecrement");
        OnPropertyChanged("CanExecuteIncrement");
        OnPropertyChanged("Counter");
      }
    }
  }
  [Command]
  public void Increment()
  {
    this.Counter++;
  }
  public bool CanExecuteIncrement => this.Counter < 10;
  [Command]
  public void Decrement()
  {
    this.Counter--;
  }
  public bool CanExecuteDecrement => this.Counter > 0;
  public MyWindow()
  {
    IncrementCommand = DelegateCommandFactory.CreateDelegateCommand(Increment, () => CanExecuteIncrement, this, "CanExecuteIncrement");
    DecrementCommand = DelegateCommandFactory.CreateDelegateCommand(Decrement, () => CanExecuteDecrement, this, "CanExecuteDecrement");
  }
  public DelegateCommand DecrementCommand { get; }
  public DelegateCommand IncrementCommand { get; }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}