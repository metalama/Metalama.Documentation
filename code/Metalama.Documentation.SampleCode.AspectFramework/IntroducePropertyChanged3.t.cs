using System.ComponentModel;
namespace Doc.IntroducePropertyChanged3;
[IntroducePropertyChangedAspect]
internal class Foo
{
  protected virtual void OnPropertyChanged(string propertyName)
  {
    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}