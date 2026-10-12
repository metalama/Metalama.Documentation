// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;
using System.Threading;

namespace Doc.LAMA5208.Error;

public class SearchViewModel
{
    // Error: the last parameter is a CancellationToken, but the method is synchronous.
    [Command]
    private void Search( CancellationToken cancellationToken ) { }
}
