﻿// This is public domain Metalama sample code.

namespace Doc.RegisterInstance
{
    [RegisterInstance]
    internal class DemoClass
    {
        public DemoClass() : base() { }

        public DemoClass( int i ) : this() { }

        public DemoClass( string s ) { }
    }
}