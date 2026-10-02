// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace DebugDemo
{
    public class BaseClass
    {
        public void Init()
        {
            Console.WriteLine( "Inside Init of BaseClass" );
        }
    }

    public class Derived1 : BaseClass
    {
        public void InitOwn() { }
    }

    public class Derived2 : BaseClass { }

    public class AnotherBaseClass
    {
        public void DoingSomething() { }
    }
}