// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.Architecture.Type_ForTestOnly
{
    public class Foo
    {
        private bool _isTest;

        public Foo() { }

        [CanOnlyBeUsedFrom(
            Namespaces = new[] { "**.Tests" },
            Description = "Use this constructor in tests only." )]
        public Foo( bool isTest )
        {
            this._isTest = isTest;
        }
    }

    internal class ForbiddenClass
    {
        // This call is forbidden because it is not in a **.Tests namespace.
        private Foo _c = new( true );
    }

    namespace Tests
    {
        internal class TestClass
        {
            // This call is allowed.
            private Foo _c = new( true );
        }
    }
}