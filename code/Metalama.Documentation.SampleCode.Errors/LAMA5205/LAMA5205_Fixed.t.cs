using Metalama.Patterns.Wpf;
namespace Doc.LAMA5205.Fixed;
public class Document
{
  public bool IsModified { get; set; }
  [Command]
  private void Save()
  {
  }
  // Fixed: only one method matches the can-execute role.
  private bool CanSave() => this.IsModified;
  public Document()
  {
    SaveCommand = DelegateCommandFactory.CreateDelegateCommand(Save, CanSave);
  }
  public DelegateCommand SaveCommand { get; }
}