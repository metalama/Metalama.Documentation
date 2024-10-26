using System.Windows;
using Metalama.Patterns.Wpf;
namespace Doc.Command.CanExecute_Czech;
public class MojeOkno : Window
{
  public int Počitadlo { get; private set; }
  [Command]
  public void VykonatZvýšení()
  {
    this.Počitadlo++;
  }
  public bool MůžemeVykonatZvýšení => this.Počitadlo < 10;
  [Command]
  public void Snížit()
  {
    this.Počitadlo--;
  }
  public bool MůžemeSnížit => this.Počitadlo > 0;
  public MojeOkno()
  {
    VykonatZvýšeníPříkaz = DelegateCommandFactory.CreateDelegateCommand(VykonatZvýšení, () => MůžemeVykonatZvýšení);
    SnížitPříkaz = DelegateCommandFactory.CreateDelegateCommand(Snížit, () => MůžemeSnížit);
  }
  public DelegateCommand SnížitPříkaz { get; }
  public DelegateCommand VykonatZvýšeníPříkaz { get; }
}