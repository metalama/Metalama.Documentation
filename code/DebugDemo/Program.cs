// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.QuickStart;
using System.Diagnostics;

namespace DebugDemo
{
    public static class Demo
    {
        public static void Main()
        {
            Debugger.Break();
            DoThis();
        }

        [Log]
        public static void DoThis()
        {
            Console.WriteLine( "Doing this" );
            DoThat();
        }

        [Log]
        public static void DoThat()
        {
            Console.WriteLine( "Doing that" );
        }
    }
}