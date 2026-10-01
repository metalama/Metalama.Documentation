using System.ComponentModel;
namespace Doc.IntroducePropertyChanged1;
[IntroducePropertyChangedAspect]
internal class Foo
{
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}