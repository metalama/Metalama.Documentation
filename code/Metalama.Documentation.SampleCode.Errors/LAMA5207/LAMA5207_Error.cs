// This is public domain Metalama sample code.

using Metalama.Patterns.Wpf;

namespace Doc.LAMA5207.Error;

public class Document
{
    // Error: the name doesn't match the pattern ^Run(?<CommandName>.+)$ and the default naming convention was removed.
    [Command]
    private void ExecuteSave() { }
}
