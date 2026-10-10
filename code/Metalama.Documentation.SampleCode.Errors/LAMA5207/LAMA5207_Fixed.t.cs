using Metalama.Patterns.Wpf;
namespace Doc.LAMA5207.Fixed;
public class Document
{
  // Fixed: the name matches the pattern of the naming convention.
  [Command]
  private void RunSave()
  {
  }
  public Document()
  {
    SaveCommand = DelegateCommandFactory.CreateDelegateCommand(RunSave, null);
  }
  public DelegateCommand SaveCommand { get; }
}