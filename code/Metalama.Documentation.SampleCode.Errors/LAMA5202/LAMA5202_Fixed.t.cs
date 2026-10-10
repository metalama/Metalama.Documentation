using Metalama.Patterns.Wpf;
using System.ComponentModel;
namespace Doc.LAMA5202.Fixed;
public partial class DocumentViewModel : INotifyPropertyChanged
{
  private bool _isDirty = true;
  public event PropertyChangedEventHandler? PropertyChanged;
  // Fixed: the can-execute property is public.
  public bool IsSaveEnabled => this._isDirty;
  [Command]
  public void Save() => this._isDirty = false;
  public DocumentViewModel()
  {
    SaveCommand = DelegateCommandFactory.CreateDelegateCommand(Save, () => IsSaveEnabled, this, "IsSaveEnabled");
  }
  public DelegateCommand SaveCommand { get; }
}