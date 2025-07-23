using System.ComponentModel;
namespace Doc.IntroducePropertyChanged2;
[IntroducePropertyChangedAspect]
internal class Foo
{
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke((object? )this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}