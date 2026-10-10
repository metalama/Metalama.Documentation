using Metalama.Patterns.Wpf;
using System.Threading;
namespace Doc.LAMA5208.Fixed;
public class SearchViewModel
{
  // Fixed: the command runs in a background thread, so it supports cancellation.
  [Command(Background = true)]
  private void Search(CancellationToken cancellationToken)
  {
  }
  public SearchViewModel()
  {
    SearchCommand = DelegateCommandFactory.CreateBackgroundDelegateCommand(Search, null, false);
  }
  public AsyncDelegateCommand SearchCommand { get; }
}