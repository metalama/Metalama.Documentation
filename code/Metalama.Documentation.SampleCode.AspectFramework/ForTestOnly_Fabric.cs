// This is public domain Metalama sample code.

using Doc.ForTestOnly_Fabric.ValidatedNamespace;

namespace Doc.ForTestOnly_Fabric
{
    namespace ValidatedNamespace
    {
        public class MyService { }
    }

    internal class NormalClass
    {
        // Usage NOT allowed here.
        private MyService _service = new();
    }

    namespace Tests
    {
        internal class TestClass
        {
            // Usage allowed here because we are in the Tests namespace.
            private MyService _service = new();
        }
    }
}