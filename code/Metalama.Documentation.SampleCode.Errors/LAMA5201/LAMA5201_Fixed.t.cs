using Metalama.Patterns.Wpf;
namespace Doc.LAMA5201.Fixed;
public partial class DocumentViewModel
{
  private bool _isDirty = true;
  private bool CanSave() => this._isDirty;
  private bool IsSaveEnabled => this._isDirty;
  // Fixed: only CanExecuteMethod is set.
  [Command(CanExecuteMethod = "CanSave")]
  public void Save() => this._isDirty = false;
  public DocumentViewModel()
  {
    SaveCommand = DelegateCommandFactory.CreateDelegateCommand(Save, CanSave);
  }
  public DelegateCommand SaveCommand { get; }
}