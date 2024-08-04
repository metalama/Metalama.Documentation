﻿namespace Doc.Decoupled_Ref;

public class C
{
    public void UnmarkedMethod() { }

    [Log( Category = "Foo" )]
    public void MarkedMethod() { }
    
    [Log( Category = "Bar" )]
    public string MarkedProperty { get; set; }
}