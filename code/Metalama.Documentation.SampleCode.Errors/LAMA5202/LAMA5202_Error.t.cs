// Warning LAMA5202 on `IsSaveEnabled`: `The can-execute property for command method DocumentViewModel.Save() is not public, and INotifyPropertyChanged integration is enabled and applicable. Because the can-execute property is not public, INotifyPropertyChanged.PropertyChanged events might not be raised depending on the INotifyPropertyChanged implementation.`
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