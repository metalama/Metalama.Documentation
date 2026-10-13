// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @DisableCompareProgramOutput
#endif

using System;
using System.Threading;

namespace Doc.Profile;

public class Program
{
    [Profile]
    public static int SimulatedDelay()
    {
        // Simulating a random delay between 500 ms to 2 secs
        Thread.Sleep( new Random().Next( 500, 2000 ) );

        return 0;
    }

    public static void Main()
    {
        SimulatedDelay();
    }
}