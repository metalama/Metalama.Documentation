// This is public domain Metalama sample code.

namespace Doc.Decoupled;

public class C
{
    public void UnmarkedMethod() { }

    [Log( Category = "Foo" )]
    public void MarkedMethod() { }
    
    [Log( Category = "Bar" )]
    public string MarkedProperty { get; set; }
}