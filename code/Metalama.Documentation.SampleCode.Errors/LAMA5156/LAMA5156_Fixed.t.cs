using Metalama.Patterns.Observability;
using System.ComponentModel;
namespace Doc.LAMA5156.Fixed;
public abstract class ViewModelBase : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;
  protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
  {
    this.PropertyChanged?.Invoke(this, args);
  }
  // Fixed: the base class defines an OnPropertyChanged(string) method.
  protected void OnPropertyChanged(string propertyName)
  {
    this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
  }
}
[Observable]
public partial class CustomerViewModel : ViewModelBase
{
  private string? _name;
  public string? Name
  {
    get
    {
      return _name;
    }
    set
    {
      if (!object.ReferenceEquals(value, _name))
      {
        _name = value;
        OnPropertyChanged("Name");
      }
    }
  }
  protected override void OnPropertyChanged(PropertyChangedEventArgs args)
  {
    base.OnPropertyChanged(args);
  }
}