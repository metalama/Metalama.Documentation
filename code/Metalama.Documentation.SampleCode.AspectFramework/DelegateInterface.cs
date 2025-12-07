// This is public domain Metalama sample code.

namespace Doc.DelegateInterface;

public interface IGreeter
{
    string Name { get; set; }

    string Greet( string message );
}

internal class InternalGreeter : IGreeter
{
    public string Name { get; set; } = "World";

    public string Greet( string message ) => $"{message}, {this.Name}!";
}

// The aspect implements IGreeter by delegating to the _greeter field.
[DelegateInterface( typeof(IGreeter), "_greeter" )]
public partial class MyService
{
    private readonly IGreeter _greeter = new InternalGreeter();
}
