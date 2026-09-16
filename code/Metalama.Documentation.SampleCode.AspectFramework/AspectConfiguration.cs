// This is public domain Metalama sample code.

namespace Doc.AspectConfiguration
{
    // Some target code.
    public class SomeClass
    {
        [Log]
        public void SomeMethod() { }
    }

    namespace ChildNamespace
    {
        public class SomeOtherClass
        {
            [Log]
            public void SomeMethod() { }
        }
    }
}