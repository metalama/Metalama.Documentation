// Warning LAMA5202 on `IsSaveEnabled`: `The can-execute property of the command method 'DocumentViewModel.Save()' is not public, but INotifyPropertyChanged integration is enabled. Because INotifyPropertyChanged implementations usually raise the PropertyChanged event only for public properties, the CanExecuteChanged event of the command might not be raised. Make the property public or disable INotifyPropertyChanged integration.`
using Metalama.Patterns.Wpf;
using System.ComponentModel;
namespace Doc.LAMA5202.Error;
public partial class DocumentViewModel : INotifyPropertyChanged
{
  private bool _isDirty = true;
  public event PropertyChangedEventHandler? PropertyChanged;
  // Warning: the can-execute property is not public.
  private bool IsSaveEnabled => this._isDirty;
  [Command]
  public void Save() => this._isDirty = false;
  public DocumentViewModel()
  {
    SaveCommand = DelegateCommandFactory.CreateDelegateCommand(Save, () => IsSaveEnabled, this, "IsSaveEnabled");
  }
  public DelegateCommand SaveCommand { get; }
}