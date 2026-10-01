// This is public domain Metalama sample code.

namespace Doc.AspectConfiguration_ProjectDefault
{
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