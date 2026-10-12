// This is public domain Metalama sample code.

namespace Doc.SimpleNotNull;

public class TheClass
{
    [NotNull]
    public string Field = "Field";

    [NotNull]
    public string Property { get; set; } = "Property";

    public void Method( [NotNull] string parameter ) { }
}