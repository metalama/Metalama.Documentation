// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.Threading;

namespace Doc.LAMA5208.Fixed;

public class SearchViewModel
{
    // Fixed: the command runs in a background thread, so it supports cancellation.
    [Command( Background = true )]
    private void Search( CancellationToken cancellationToken ) { }
}
