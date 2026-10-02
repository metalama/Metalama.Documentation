// This is public domain Metalama sample code.

using System.Diagnostics;

namespace Doc.AspectConfiguration_Provider;

[LogConfiguration( Category = "SomeClass" )]
public class SomeClass
{
    [Log]
    public void SomeMethod() { }
}

[LogConfiguration( Category = "SomeClass" )]
public class ChildNamespace
{
    public class SomeOtherClass
    {
        [Log( Level = TraceLevel.Warning )]
        public void SomeMethod() { }
    }
}